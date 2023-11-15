function validation() {

    var filePath = document.getElementById("fileentitycreatefromfilepath").value;
    var desc = document.getElementById("fileentitycreatefromdesc").value;
    var file = document.getElementById("fileentitycreatefromfile").value;


    if (filePath.length > 150 || file.path < 3) {
        sweetAlert({
            title: "Description must be less than 180 and more than 3 characters",
            text: "",
            icon: "error",
            timer: 5000,
            showConfirmButton: false
        });
        return false;
    } else if (desc.length > 180 || desc.length < 3) {
        sweetAlert({
            title: "file path must be less than 150 and more than 3 characters",
            text: "",
            icon: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (file == null) {
        sweetAlert({
            title: "file path must be less than 150 characters",
            text: "",
            icon: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    }else {
        sweetAlert({
            title: "Done!",
            text: "Record Added",
            icon: "success",
            timer: 4000,
            showConfirmButton: false
        });
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


document.getElementById("fileentitycreatefrom").onsubmit = function () { return validation(); };

document.getElementById("fileentitycreatefromfile").onchange = function () {
    document.getElementById("createfileentityselectfile").innerHTML = "Selected";
}
document.getElementById("pathsuggestion").onchange = function () {

    document.getElementById("fileentitycreatefromfilepath").value = document.getElementById("pathsuggestion").options[document.getElementById("pathsuggestion").selectedIndex].text;
    CountCharsFilePath();
};
