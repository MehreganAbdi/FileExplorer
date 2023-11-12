function validation() {
    var name = document.myform.name.value;
    var filePath = document.myform.filepath.value;
    var desc = document.myform.description.value;
    var file = document.myform.filetocopy;


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
            text: "Record Added",
            icon: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}

function CountCharsName(obj) {
    document.getElementById("name_count").innerHTML = '100 . ' + obj.value.length;
}
function CountCharsFilePath(obj) {
    document.getElementById("filepath_count").innerHTML = '180 . ' + obj.value.length;

}
function CountCharsDesc(obj) {
    document.getElementById("desc_count").innerHTML = '150 . ' + obj.value.length;

}