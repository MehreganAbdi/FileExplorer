function validation() {

    var filePath = document.getElementById("fileentitycreatefromfilepath").value;
    var desc = document.getElementById("fileentitycreatefromdesc").value;
    var file = document.getElementById("fileentitycreatefromfile").value;


    if (filePath.length > 150 || file.path < 3) {
        sweetAlert({
            title: "filePath must be less than 180 and more than 3 characters",
            text: "",
            type: "error",
            timer: 5000,
            showConfirmButton: false
        });
        return false;
    } else if (desc.length > 180 || desc.length < 3) {
        sweetAlert({
            title: "Description must be less than 150 and more than 3 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (file == null) {
        sweetAlert({
            title: "Select A File First",
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


function CountCharsFilePath() {
    document.getElementById("filepathcount").innerHTML = '180 / ' + document.getElementById("fileentitycreatefromfilepath").value.length;

}
function CountCharsDesc() {
    document.getElementById("desccount").innerHTML = '150 / ' + document.getElementById("fileentitycreatefromdesc").value.length;

}

document.getElementById("fileentitycreatefromdesc").onkeyup = function () { return CountCharsDesc() };

document.getElementById("fileentitycreatefromfilepath").onkeyup = function () { return CountCharsFilePath() };



document.getElementById("fileentitycreatefromfile").onchange = function () {
    document.getElementById("createfileentityselectfile").innerHTML = "Selected";
}
document.getElementById("pathsuggestion").onchange = function () {

    document.getElementById("fileentitycreatefromfilepath").value = document.getElementById("pathsuggestion").options[document.getElementById("pathsuggestion").selectedIndex].text;
    CountCharsFilePath();
};

document.getElementById("fileentitycreateformsubmit").onclick = function () {
    if (validation()) {

        let urel = "https://localhost:7242/FileEntity/Create/"

        var valdata = $("#fileentitycreatefrom").serialize();

       

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




};