
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class NotesController : ApiController
    {


        NotesDBEntities myDB = new NotesDBEntities();
        public IEnumerable<Note> GetAllNotes()
        {
            List<Note> returnList = myDB.Notes.ToList();
            returnList.Sort();
            return returnList;
        }

        public IHttpActionResult GetNote(string id)
        {
            Note note = myDB.Notes.FirstOrDefault((p) => p.Subject == id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public IHttpActionResult Save(Note newNote)
        {
            myDB.Notes.Add(newNote);
            myDB.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            bool found = true;
            string subject = id;

            // Remove the entity from the entity collection  
            Note note = myDB.Notes.FirstOrDefault((p) => p.Subject == subject);
            if (note != null)
            {
                myDB.Notes.Remove(note);
                myDB.SaveChanges();
            }
            else
            {
                found = false;
            }

            HttpResponseMessage response = new HttpResponseMessage();
            if (!found)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }
    }
   
}
