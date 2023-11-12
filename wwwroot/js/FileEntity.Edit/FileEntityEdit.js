
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
    } else if (desc.length > 180) {
        sweetAlert({
            title: "Description must be less than 180 characters",
            text: "",
            icon: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else if (filePath.length > 150) {
        sweetAlert({
            title: "file path must be less than 150 characters",
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
    document.getElementById("name_count").innerHTML = '100 . ' + document.getElementById("fileentityeditformname").value.length;
}
function CountCharsFilePath() {
    document.getElementById("filepath_count").innerHTML = '180 / ' + document.getElementById("fileentityeditformfilepath").value.length;

}
function CountCharsDesc() {
    document.getElementById("desc_count").innerHTML = '150 / ' + document.getElementById("fileentityeditformdesc").value.length;

}
