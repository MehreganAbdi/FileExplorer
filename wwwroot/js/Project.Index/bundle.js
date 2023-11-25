$(document).ready(function () {

    $.ajax({

        url: 'Project/GetAllProjectsInJson',
        type: 'GET',
        success: function (json) {
            var tr;

            for (var i = 0; i < json.length; i++) {
                var j = i + 1;
                tr = $('<tr/>');
                tr.append("<td class='table-info' ><center>" + j + "</td></center>")
                tr.append("<td><center>" + json[i].id + "</center></td>");
                tr.append("<td><center>" + json[i].projectName + "</center></td>");
                tr.append('<td><center><a type="button" href="Project/Edit/' + json[i].id + '" class="btn-edit">Edit</a></center></td>');
                tr.append(' <td><center><input data-id="' + json[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
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

    var searchValue = document.getElementById("projectindexsearch").value;

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
                    tr.append("<td class='table-info' ><center>" + j + "</td></center>")
                    tr.append("<td><center>" + json[i].id + "</center></td>");
                    tr.append("<td><center>" + json[i].projectName + "</center></td>");
                    tr.append('<td><center><a type="button" href="Project/Edit/' + json[i].id + '" class="btn-edit">Edit</a></center></td>');
                    tr.append(' <td><center><input data-id="' + json[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
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


}

//document.getElementById("projectindexsearch").onkeyup = function () {

//    if (document.getElementById("projectindexsearch").value != "") {
//        var search = document.getElementById("projectindexsearch").value;
//        document.getElementById("projectindextable").innerHTML = "";
//        $.ajax({

//            url: 'Project/GetAllProjectsInJson',
//            type: 'GET',
//            success: function (json) {
//                var tr;
//                var j = 1;

//                for (var i = 0; i < json.value.length; i++) {

//                    if (json.value[i].projectName.includes(search)) {

//                        tr = $('<tr/>');
//                        tr.append("<td class='table-info'  ><center>" + j + "</td></center>")
//                        tr.append("<td><center>" + json.value[i].id + "</center></td>");
//                        tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
//                        tr.append('<td><center><a type="button" href="Project/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
//                        tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
//                        $('table').append(tr);
//                        j++;
//                    }
//                }

//                const allDeleteButtons = document.querySelectorAll('.project-index-delete');
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

//            url: 'Project/GetAllProjectsInJson',
//            type: 'GET',
//            success: function (json) {
//                var tr;

//                for (var i = 0; i < json.value.length; i++) {
//                    var j = i + 1;
//                    tr = $('<tr/>');
//                    tr.append("<td class='table-info'  ><center>" + j + "</td></center>")
//                    tr.append("<td><center>" + json.value[i].id + "</center></td>");
//                    tr.append("<td><center>" + json.value[i].projectName + "</center></td>");
//                    tr.append('<td><center><a type="button" href="Project/Edit/' + json.value[i].id + '" class="btn-edit">Edit</a></center></td>');
//                    tr.append(' <td><center><input data-id="' + json.value[i].id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
//                    $('table').append(tr);
//                }

//                const allDeleteButtons = document.querySelectorAll('.project-index-delete');
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
//    location.reload();
//}




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

    var projectName = document.getElementById("projecteditformname").value;

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
    document.getElementById("editprojectnc").innerHTML = '100 / ' + document.getElementById("projecteditformname").value.length;
}


document.getElementById("projecteditformname").onkeyup = function () { return CountCharsEditProject(); };

document.getElementById("editprojectsubmit").onclick = function () {
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
}