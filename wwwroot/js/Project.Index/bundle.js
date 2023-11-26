$(document).ready(function () {

    $.ajax({

        url: 'Project/GetAllProjectsInJson',
        type: 'GET',
        success: function (json) {
            var tr;

            for (var i = 0; i < json.length; i++) {
                var j = i + 1;
                tr = $('<tr/>');
                tr.append("<td class='table-secondary' ><center>" + j + "</td></center>")
                tr.append("<td><center>" + json[i].id + "</center></td>");
                tr.append("<td><center>" + json[i].projectName + "</center></td>");
                tr.append('<td><center><a type="button" href="Project/Edit/' + json[i].id + '" class="btn btn-outline-dark">Edit</a></center></td>');
                tr.append(' <td><center><input data-id="' + json[i].id + '" type="button" id="deleteproject" class="btn btn-outline-danger" value="Delete" /></center></td>');
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


$("#projectindexsearch").keyup ( function () {

    var searchValue = $("#projectindexsearch").val();

    if (searchValue == "") {

        document.location.reload();

    } else {

        $.ajax({
            url: "Project/GetAllProjectsInJson/" + searchValue,
            method: "GET",

            success: function (json) {

                var tr;
                $('table tbody').html("");

                for (var i = 0; i < json.length; i++) {
                    var j = i + 1;
                    tr = $('<tr/>');
                    tr.append("<td class='table-secondary' ><center>" + j + "</td></center>")
                    tr.append("<td><center>" + json[i].id + "</center></td>");
                    tr.append("<td><center>" + json[i].projectName + "</center></td>");
                    tr.append('<td><center><a type="button" href="Project/Edit/' + json[i].id + '" class="btn btn-outline-dark">Edit</a></center></td>');
                    tr.append(' <td><center><input data-id="' + json[i].id + '" type="button" id="deleteproject" class="btn btn-outline-danger" value="Delete" /></center></td>');
                    $('table tbody').append(tr);
                }

                const allDeleteButtons = document.querySelectorAll('.project-index-delete');
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

    }


})


function validation() {
    var projectName = $("#createprojectformname").val();



    if (projectName.length < 3 || projectName.length > 100) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else {

       
        return true;
    }


}


function CountCharsProject() {
    $("#projectnamecount").html( '100 / ' + $("#createprojectformname").val().length);
}
$("#createprojectformname").keyup (function () { return CountCharsProject() });

$("#createpeojectsubmit").click (function () {
    if (validation()) {

        let urel = "https://localhost:7242/Project/Create/"

        var valdata = $("#createprojectform").serialize();

        sweetAlert({
            title: "Are you sure?",
            text: "",
            type: "warning",
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: "Yes, Submit It",
            cancelButtonText: "No",

        }).then(async function (result) {
            if (result.dismiss != 'cancel') {

                $.ajax({
                    url: urel,
                    type: 'POST',
                    data: valdata,
                    success: function () {
                        sweetAlert({
                            title: "Done!",
                            text: "Form Submitted Successfully",
                            type: "success"
                        });

                        document.getElementById("redirecttoprojectindex").style = "";
                    },
                    error: function () {
                        sweetAlert({
                            title: "Failed",
                            text: "Form Coudn't get Submit",
                            type: "error"
                        });
                    }
                });

             


            }
            else {
                sweetAlert("Canceled", "", "success")

            }


        })


    }
    else {
        return validation();
    }
})
function validation() {

    var projectName = $("#projecteditformname").val();

    if (projectName.length < 3 || projectName.length > 100) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            type: "error",
            timer: 5000,
            showConfirmButton: false
        });

        return false;
    } else {
        sweetAlert({
            title: "Done!",
            text: "Project Updated",
            type: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}
function CountCharsEditProject() {
    $("#editprojectnc").html( '100 / ' + $("#projecteditformname").val().length);
}


$("#projecteditformname").keyup (function () { return CountCharsEditProject(); });


$("#editprojectsubmit").click (function () {
    if (validation()) {

        let urel = "https://localhost:7242/Project/Edit/"

        var valdata = $("#editprojectform").serialize();

        sweetAlert({
            title: "Are you sure?",
            text: "",
            type: "warning",
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: "Yes, Apply The Changes",
            cancelButtonText: "No",

        }).then(async function (result) {
            if (result.dismiss != 'cancel') {

                $.ajax({
                    url: urel,
                    type: 'POST',
                    data: valdata,
                    success: function () {
                        sweetAlert({
                            title: "Done!",
                            text: "Changes Updated Successfully",
                            type: "success"
                        });

                        document.getElementById("redirecttoprojectindex").style = "";
                    },
                    error: function () {
                        sweetAlert({
                            title: "Failed",
                            text: "Form Coudn't get Submit",
                            type: "error"
                        });
                    }
                });

              

            }
            else {
                sweetAlert("Canceled", "", "success")

            }


        })


    }
    else {
        return validation();
    }
})