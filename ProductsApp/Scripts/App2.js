
var uri = 'api/Notes';

$(document).on('pagebeforeshow ', '#pageone', function () {   // see: https://stackoverflow.com/questions/14468659/jquery-mobile-document-ready-vs-page-events
 
    GetShowData();
   
});




function GetShowData() {
    $("#notes").empty();
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            // had to add encodeURI to my data-parm to not lose spaces and text 
            $.each(data, function (index, record) {   // make up each li as an <a> to the details-page
                $('#notes').append('<li><a data-transition="pop" data-parm=' + encodeURI(record.Subject) + ' href="#details-page">' + record.Priority + ' => ' + record.Subject + '</a></li>');
                console.log(record.Subject);
            });
            // // need this so jquery mobile will apply the styling to the newly added li's
            $("#notes").listview("refresh");
            $("a").on("click", function (event) {    // set up an event, if user clicks any, it writes that items data-parm into the 
                var parm = $(this).attr("data-parm");  // passing in the record.Subject
                //grab this list items data-parm and write it into the details page
                $("#detailParmHere").html(parm);
            });
        });
}


$(document).on('pagebeforeshow', '#details-page', function () {
    var vPriority;
    var vSubject;
    var vDetails;
    var id = $('#detailParmHere').text(); // get the subject text out of our hidden element
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            vPriority = "Priority: " + data.Priority;
            vSubject = " Subject: " + data.Subject;
            vDetails = " Details: " + data.Details;
            console.log(vPriority);
            console.log(vSubject);
            console.log(vDetails);
            $('#showdata').text(vPriority).append('<br />');; //display to details page
            $('#showdata').append(vSubject).append('<br />');;
            $('#showdata').append(vDetails);
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#showdata').text('Error: ' + err);
        });

});



