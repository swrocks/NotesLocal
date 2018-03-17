
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

    }
}
