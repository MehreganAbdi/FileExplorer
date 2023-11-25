
function validation() {
    var name = $("#fileentityeditformname").val();
    var filePath = $("#fileentityeditformfilepath").val();
    var desc = $("#fileentityeditformdesc").val();





    if (name.length > 100 || name.length < 3) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (desc.length > 180 || desc.length < 3) {
        sweetAlert({
            title: "Description must be less than 180 and more than 3 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (filePath.length > 150 || filePath.length < 3) {
        sweetAlert({
            title: "file path must be less than 150 and more than 3 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else {
        sweetAlert({
            title: "Done!",
            text: "Record Updated",
            type: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}

function CountCharsName() {
    $("#namecount").html('100 / ' + $("#fileentityeditformname").val().length);
}
function CountCharsFilePath() {
    $("#filepathcount").html( '180 / ' + ("#fileentityeditformfilepath").val().length);

}
function CountCharsDesc() {
    $("#desccount").html( '150 / ' + $("#fileentityeditformdesc").val().length);

}
$("#fileentityeditformname").keyup (function () { return CountCharsName() });

$("#fileentityeditformfilepath").keyup (function () { return CountCharsFilePath() });

$("#fileentityeditformdesc").keyup (function () { return CountCharsDesc() });



$("#editfileentitysubmit").click ( function () {
    if (validation()) {

        let urel = "https://localhost:7242/FileEntity/Edit/"

        var valdata = $("#editfileentityform").serialize();

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

                        document.getElementById("redirecttofileentityindex").style = "";
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