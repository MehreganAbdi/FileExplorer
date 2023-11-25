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