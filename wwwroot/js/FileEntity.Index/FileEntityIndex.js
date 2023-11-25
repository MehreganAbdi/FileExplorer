$(document).ready(function () {

    $.ajax({

        url: 'FileEntity/GetAllRecordsInJson',
        type: 'GET',
        success: function (json) {
            var tr;

            for (var i = 0; i < json.value.length; i++) {
                var j = i + 1;
                tr = $('<tr/>');
                tr.append("<td class='table-info'><center>" + j + "</td></center>")
                tr.append("<td><center>" + json.value[i].name + "</center></td>");
                tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
                tr.append("<td><center>" + json.value[i].dateCreated + "</center></td>");
                tr.append("<td><center>" + json.value[i].filePath + "</center></td>");
                tr.append("<td><center>" + json.value[i].description + "</center></td>");
                tr.append("<td><center>" + json.value[i].size + "</center></td>");

                tr.append('<td><center><a type="button" href="FileEntity/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
                tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete file-index-delete" value="Delete" /></center></td>');
                $('table').append(tr);
            }

            const allDeleteButtons = document.querySelectorAll('.file-index-delete');
            for (const delbtn of allDeleteButtons) {
                delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
            }


        },
        error: function () {
            sweetAlert({
                title: "Failed",
                text: "Data Or Connection Have Errors",
                type: "error"
            });
        }

    });



});



function deleteConfirm(id) {

    let url = "https://localhost:7242/FileEntity/Delete/" + id;

    sweetAlert({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover\n",
        type: "warning",
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: "Yes, Delete It",
        cancelButtonText: "No"
    }).then(async function (result) {
        if (result.dismiss != 'cancel') {
            const response = await fetch(url, {
                method: 'GET'

            });

            if (response.ok) {
                sweetAlert({
                    title: "Done!",
                    text: "Record Deleted Successfully",
                    type: "success"
                });
            } else {
                sweetAlert("Error Occured", "Record Didn't Exist Or An Error Occurred", "error")
            }
            document.location.reload();

        } else {
            sweetAlert("Proccess Canceled ", "Record Is Safe", "success");
        }

    });
   
}


$("#fileentityindexsearch").keyup ( function () {

    var searchValue = document.getElementById("fileentityindexsearch").value;


    if (searchValue != null) {


        $('table tbody').html("");
        $.ajax({
            url: "FileEntity/GetAllRecordsInJson/" + searchValue,
            method: "GET",

            success: function (json) {

                var tr;

                for (var i = 0; i < json.value.length; i++) {
                    var j = i + 1;
                    tr = $('<tr/>');
                    tr.append("<td class='table-info' ><center>" + j + "|</td></center>")
                    tr.append("<td><center>" + json.value[i].name + "</center></td>");
                    tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
                    tr.append("<td><center>" + json.value[i].dateCreated + "</center></td>");
                    tr.append("<td><center>" + json.value[i].filePath + "</center></td>");
                    tr.append("<td><center>" + json.value[i].description + "</center></td>");
                    tr.append("<td><center>" + json.value[i].size + "</center></td>");

                    tr.append('<td><center><a type="button" href="FileEntity/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
                    tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete file-index-delete" value="Delete" /></center></td>');
                    $('table tbody').append(tr);
                }

                const allDeleteButtons = document.querySelectorAll('.file-index-delete');
                for (const delbtn of allDeleteButtons) {
                    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
                }


            },
            error: function () {
                sweetAlert({
                    title: "Proccess Failed",
                    type: 'error',

                })
            }
        })
    } else {
        document.location.reload();
    }
})
