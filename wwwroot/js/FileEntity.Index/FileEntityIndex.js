$(document).ready(function () {

    $.ajax({

        url: 'FileEntity/GetAllRecordsInJson',
        type: 'GET',
        success: function (json) {
            var tr;

            for (var i = 0; i < json.value.length; i++) {
                var j = i + 1;
                tr = $('<tr/>');
                tr.append("<td><center>" + j + "|</td></center>")
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


document.getElementById("fileentityindexsearch").onkeyup = function () {

    var searchValue = document.getElementById("fileentityindexsearch").value;


    if (searchValue != null) {


        document.getElementById("fileentityindextable").innerHTML = "";
        $.ajax({
            url: "FileEntity/GetAllRecordsInJson/" + searchValue,
            method: "GET",

            success: function (json) {

                var tr;

                for (var i = 0; i < json.value.length; i++) {
                    var j = i + 1;
                    tr = $('<tr/>');
                    tr.append("<td><center>" + j + "|</td></center>")
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
                    title: "Proccess Failed",
                    type: 'error',

                })
            }
        })
    } else {
        document.location.reload();
    }
}

//document.getElementById("fileentityindexsearch").onkeyup = function () {

//    if (document.getElementById("fileentityindexsearch").value != "") {
//        var search = document.getElementById("fileentityindexsearch").value;
//        document.getElementById("fileentityindextable").innerHTML = "";
//        $.ajax({

//            url: 'FileEntity/GetAllRecordsInJson',
//            type: 'GET',
//            success: function (json) {
//                var tr;
//                var j = 1;



//                for (var i = 0; i < json.value.length; i++) {

//                    if (json.value[i].projectName.includes(search) || json.value[i].name.includes(search) || json.value[i].filePath.includes(search)
//                        || json.value[i].description.includes(search) || json.value[i].size.includes(search)) {

//                        tr = $('<tr/>');
//                        tr.append("<td><center>" + j + "|</td></center>")
//                        tr.append("<td><center>" + json.value[i].name + "</center></td>");
//                        tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
//                        tr.append("<td><center>" + json.value[i].dateCreated + "</center></td>");
//                        tr.append("<td><center>" + json.value[i].filePath + "</center></td>");
//                        tr.append("<td><center>" + json.value[i].description + "</center></td>");
//                        tr.append("<td><center>" + json.value[i].size + "</center></td>");

//                        tr.append('<td><center><a type="button" href="FileEntity/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
//                        tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete file-index-delete" value="Delete" /></center></td>');
//                        $('table').append(tr);
//                        j++;
//                    }

//                }
               

//                const allDeleteButtons = document.querySelectorAll('.file-index-delete');
//                for (const delbtn of allDeleteButtons) {
//                    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
//                }

//            },
//            srror: function () {
//                sweetAlert({
//                    title: "Failed",
//                    text: "Form Coudn't get Submit",
//                    type: "error"
//                });
//            }

//        });

//    }
//    else {

//        $.ajax({

//            url: 'FileEntity/GetAllRecordsInJson',
//            type: 'GET',
//            success: function (json) {
//                var tr;

//                for (var i = 0; i < json.value.length; i++) {
//                    var j = i + 1;
//                    tr = $('<tr/>');
//                    tr.append("<td><center>" + j + "|</td></center>")
//                    tr.append("<td><center>" + json.value[i].name + "</center></td>");
//                    tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
//                    tr.append("<td><center>" + json.value[i].dateCreated + "</center></td>");
//                    tr.append("<td><center>" + json.value[i].filePath + "</center></td>");
//                    tr.append("<td><center>" + json.value[i].description + "</center></td>");
//                    tr.append("<td><center>" + json.value[i].size + "</center></td>");

//                    tr.append('<td><center><a type="button" href="FileEntity/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
//                    tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete file-index-delete" value="Delete" /></center></td>');
//                    $('table').append(tr);
//                }

//                const allDeleteButtons = document.querySelectorAll('.file-index-delete');
//                for (const delbtn of allDeleteButtons) {
//                    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
//                }


//            },
//            srror: function () {
//                sweetAlert({
//                    title: "Failed",
//                    text: "Form Coudn't get Submit",
//                    type: "error"
//                });
//            }

//        });

//    }
//}

