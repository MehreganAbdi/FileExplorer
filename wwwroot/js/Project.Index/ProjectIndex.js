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


        } else {
            sweetAlert("Proccess Canceled ", "Project Is Safe", "success");
        }

    });
}


const allDeleteButtons = document.querySelectorAll('.project-index-delete');
for (const delbtn of allDeleteButtons) {
    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
}

document.getElementById("getallprojectsinjson").onclick = function () {

    $.ajax({

        url: 'Project/GetAllProjectsInJson',
        type: 'GET',
        success: function (json) {
            var tr;

            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td><center>" + json[i].Id + "</center></td>");
                tr.append("<td><center>" + json[i].ProjectName + "</center></td>");
                tr.append('<td><center><a asp-action="Edit" asp-controller="Project" asp-route-id="' + json[i].Id + '" class="btn-edit">Edit</a></center></td>');
                tr.append(' <td><center><input data-id="' + json[i].Id + '" type="button" id="deleteproject" class="btn-delete project-index-delete" value="Delete" /></center></td>');
                $('table').append(tr);
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
