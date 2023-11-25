function validation() {
    var name = $("#createname");
    var filePath = $("#createfilepath");
    var desc = $("#createdescription");



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
            text: "Record Added",
            type: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}

function CountCharsName() {
$("#namecount").html( '100 / ' + $("#createname").val().length);
   
}
function CountCharsFilePath() {
    $("#filepathcount").html( '180 / ' + $("#createfilepath").val().length);

}
function CountCharsDesc() {
    $("#desccount").html( '150 / ' + $("#createdescription").val().length);

}



$("#createform").submit (function () { return validation(); });
$("#createname").keyup (function () { return CountCharsName() });
$("#createfilepath").keyup ( function () { return CountCharsFilePath(); });
$("#createdescription").keyup (function () { return CountCharsDesc(); });