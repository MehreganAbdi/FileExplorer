
function validation() {
    var name = document.getElementById("fileentityeditformname").value;
    var filePath = document.getElementById("fileentityeditformfilepath").value;
    var desc = document.getElementById("fileentityeditformdesc").value;





    if (name.length > 100 || name.length < 3) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            icon: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (desc.length > 180 || desc.length<3) {
        sweetAlert({
            title: "Description must be less than 180 and more than 3 characters",
            text: "",
            icon: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (filePath.length > 150 || dilepath.length<3) {
        sweetAlert({
            title: "file path must be less than 150 and more than 3 characters",
            text: "",
            icon: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else {
        sweetAlert({
            title: "Done!",
            text: "Record Updated",
            icon: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}

function CountCharsName() {
    document.getElementById("namecount").innerHTML = '100 / ' + document.getElementById("fileentityeditformname").value.length;
}
function CountCharsFilePath() {
    document.getElementById("filepathcount").innerHTML = '180 / ' + document.getElementById("fileentityeditformfilepath").value.length;

}
function CountCharsDesc() {
    document.getElementById("desccount").innerHTML = '150 / ' + document.getElementById("fileentityeditformdesc").value.length;

}
document.getElementById("fileentityeditformname").onkeyup = function () { return CountCharsName() };

document.getElementById("fileentityeditformfilepath").onkeyup = function () { return CountCharsFilePath() };

document.getElementById("fileentityeditformdesc").onkeyup = function () { return CountCharsDesc() };

document.getElementById("fileentityeditform").onsubmit = function () { return validation() };