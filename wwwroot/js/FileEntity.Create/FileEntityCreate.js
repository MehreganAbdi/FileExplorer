function validation() {

    var filePath = document.getElementById("fileentitycreatefromfilepath").value;
    var desc = document.getElementById("fileentitycreatefromdesc").value;
    var file = $("#fileentitycreatefromfile").prop("files")[0];


    if (filePath.length > 150 || filePath.length < 3) {
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
    } else if (file.size > 2000000 || file == null) {
        sweetAlert({
            title: "Select An Acceptable File (File Must Be < 2 Mb )",
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
        var file = $("#fileentitycreatefromfile").prop("files")[0];
        var imageResult = "";

        var imageData = new FormData();
        imageData.append("fileToUpload", jQuery("#fileentitycreatefromfile").get(0).files[0]);

        $.ajax({
            url: "https://localhost:7242/FileEntity/UploadPhotoAsync/",
            type: 'POST',
            data: imageData,
            contentType: false,
            processData: false,
            success: function (json) {

                if (json.value == 'false') {
                    sweetAlert({
                        title: "Upload Failed",
                        text: "File Value Recived File As Null",
                        type: "error"
                    });

                } else {
                    imageResult = json.value;


                }
            }

        })


        if (imageResult != "") {

            document.getElementById("fileentitycreatefromsize").value = imageResult;
        }

        document.getElementById("fileentitycreatefromname").value = file.name;
        document.getElementById("fileentitycreatefromsize").value = file.size.toString();
        document.getElementById("fileentitycreatefromtype").value = file.type;


        var valdata = $("#fileentitycreatefrom").serialize();
        //var allData = new FormData();


        //allData.append("FileToCopy", $("#fileentitycreatefromfile").prop("files")[0]);

        //allData.append("form", valdata);

        sweetAlert({
            title: "Are you sure?",
            text: "",
            type: "warning",
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: "Yes, Submit It",
            cancelButtonText: "No",

        }).then(async function (result) {
            if (result.value) {

                $.ajax({
                    url: urel,
                    type: 'POST',
                    data: valdata + '&FileToCopy=' + file,
                    enctype: 'multipart/form-data',
                    success: function () {
                        sweetAlert({
                            title: "Done!",
                            text: "Form Submitted Successfully",
                            type: "success"
                        });

                        document.getElementById("fileentitycreatereturntohome").style = "";
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