$(document).ready(function () {
    $("#pathsuggestion").select2({
        tags: true
    });

})

function validation() {

    var filePath = $("#pathsuggestion").val();
    var desc = $("#fileentitycreatefromdesc").val();
    var file = $("#fileentitycreatefromfile").prop("files")[0];
    var pathpattern = "";



    var value = $("#pathsuggestion").val();
    if (value.length > 3) {


        $.ajax({
            url: 'FileEntity/ValidatePath/' + value,
            method: 'GET',
            processData: false,
            contentType: false,
            cache: false,
            success: function (json) {

                if (json == 'true') {
                    pathpattern = 'true';
                } else {
                    pathpattern = 'false';
                }

            }
        });


    }

    
    if (filePath.length > 150 || filePath.length < 3) {
        sweetAlert({
            title: "filePath must be less than 180 and more than 3 characters",
            text: "",
            type: "error",
            timer: 5000,
            showConfirmButton: false
        });
        return false;
    } else if (pathpattern == 'false') {

        sweetAlert({
            title: "filePath Pattern Is Not Acceptable .",
            text: "",
            type: "error",
            timer: 5000,
            showConfirmButton: false
        });

        return false;

    }
    else if (desc.length > 180 || desc.length < 3) {
        sweetAlert({
            title: "Description must be less than 150 and more than 3 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if ($("#fileentitycreatefromfile").prop("files").length == 0  || file.size > 2000000) {
        sweetAlert({
            title: "Select An Acceptable File (File Must Be < 2 Mb )",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (file.type != "image/png" && file.type!="image/jpg" && file.type!="image/jpeg"  ) {
        sweetAlert({
            title: "Select An Acceptable File (File Must Be An Image)",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    }
    else {

        return true;
    }
}


function CountCharsFilePath() {
    $("#filepathcount").html('180 / ' + $("#fileentitycreatefromfilepath").val().length);

}
function CountCharsDesc() {
    $("#desccount").html('150 / ' + $("#fileentitycreatefromdesc").val().length);

}

$("#fileentitycreatefromdesc").keyup(function () { return CountCharsDesc() });



$("#pathsuggestion").keyup(function () { return CountCharsFilePath() });





$("#fileentitycreatefromfile").change(function () {
    $("#createfileentityselectfile").html("Selected");
});
$("#pathsuggestion").change ( function () {

    document.getElementById("fileentitycreatefromfilepath").value = document.getElementById("pathsuggestion").options[document.getElementById("pathsuggestion").selectedIndex].text;
    CountCharsFilePath();
});

$("#fileentitycreateformsubmit").click (function () {
    if (validation()) {

        let urel = "https://localhost:7242/FileEntity/Create/"
        var file = $("#fileentitycreatefromfile").prop("files")[0];

       

        document.getElementById("fileentitycreatefromname").value = file.name;
        document.getElementById("fileentitycreatefromsize").value = file.size.toString();
        document.getElementById("fileentitycreatefromtype").value = file.type;



        var formData = new FormData();
        formData.append("fileToCopy", file);
        formData.append("name", $("#fileentitycreatefromname").val());
        formData.append("description", $("#fileentitycreatefromdesc").val());
        formData.append("filePath", $("#pathsuggestion").val());
        formData.append("projectId", $("#projectidselect").val());
        formData.append("size", $("#fileentitycreatefromsize").val());
        formData.append("type", $("#fileentitycreatefromtype").val());

        
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
                    method: 'POST',
                    data: formData,
                    enctype: 'multipart/form-data',
                    processData: false,
                    contentType: false,
                    cache: false,
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




});