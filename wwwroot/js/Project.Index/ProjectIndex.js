$(document).ready(function () {

    $.ajax({

        url: 'Project/GetAllProjectsInJson',
        type: 'GET',
        success: function (json) {
            var tr;

            for (var i = 0; i < json.value.length; i++) {
                var j = i + 1;
                tr = $('<tr/>');
                tr.append("<td><center>" + j + "|</td></center>")
                tr.append("<td><center>" + json.value[i].id + "</center></td>");
                tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
                tr.append('<td><center><a type="button" href="Project/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
                tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
                $('table').append(tr);
            }

            const allDeleteButtons = document.querySelectorAll('.project-index-delete');
            for (const delbtn of allDeleteButtons) {
                delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
            }

          

        },
        srror: function () {
            sweetAlert({
                title: "Failed",
                text: "Form Coudn't get Submit",
                type: "error"
            });
        }

    });



});



function deleteConfirm(id) {

    let url = "https://localhost:7242/Project/Delete/" + id;

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
                    text: "Project Deleted Successfully",
                    type: "success"
                });
            } else {
                sweetAlert("Error Occured", "Project Didn't Exist Or An Error Occurred", "error")
            }
            document.location.reload();

        } else {
            sweetAlert("Proccess Canceled ", "Project Is Safe", "success");
        }

    });
}


document.getElementById("projectindexsearch").onkeyup = function () {

    if (document.getElementById("projectindexsearch").value != "") {
        var search = document.getElementById("projectindexsearch").value;
        document.getElementById("projectindextable").innerHTML = "";
        $.ajax({

            url: 'Project/GetAllProjectsInJson',
            type: 'GET',
            success: function (json) {
                var tr;
                var j = 1;
                
                for (var i = 0; i < json.value.length; i++) {

                    if (json.value[i].projectName.includes(search)) {

                        tr = $('<tr/>');
                        tr.append("<td><center>" + j + "|</td></center>")
                        tr.append("<td><center>" + json.value[i].id + "</center></td>");
                        tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
                        tr.append('<td><center><a type="button" href="Project/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
                        tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
                        $('table').append(tr);
                        j++;
                    }
                }

                const allDeleteButtons = document.querySelectorAll('.project-index-delete');
                for (const delbtn of allDeleteButtons) {
                    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
                }

                

            },
            srror: function () {
                sweetAlert({
                    title: "Failed",
                    text: "Form Coudn't get Submit",
                    type: "error"
                });
            }

        });

    }
    else {

        $.ajax({

            url: 'Project/GetAllProjectsInJson',
            type: 'GET',
            success: function (json) {
                var tr;

                for (var i = 0; i < json.value.length; i++) {
                    var j = i + 1;
                    tr = $('<tr/>');
                    tr.append("<td><center>" + j + "|</td></center>")
                    tr.append("<td><center>" + json.value[i].id + "</center></td>");
                    tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
                    tr.append('<td><center><a type="button" href="Project/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
                    tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
                    $('table').append(tr);
                }

                const allDeleteButtons = document.querySelectorAll('.project-index-delete');
                for (const delbtn of allDeleteButtons) {
                    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
                }


            },
            srror: function () {
                sweetAlert({
                    title: "Failed",
                    text: "Form Coudn't get Submit",
                    type: "error"
                });
            }

        });

    }
    location.reload();
}


