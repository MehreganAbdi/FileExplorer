
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

                        $("#redirecttoprojectindex").attr("style","");
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