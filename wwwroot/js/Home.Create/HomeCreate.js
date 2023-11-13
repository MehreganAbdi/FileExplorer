


function validation() {
    var name = document.getElementById("createname");
    var filePath = document.getElementById("createfilepath");
    var desc = document.myform.getElementById("createdescription");



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
    } else if (filePath.length > 150) {
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
    document.getElementById("namecount").innerHTML = '100 / ' + document.getElementById("createname").value.length;
   
}
function CountCharsFilePath() {
    document.getElementById("filepathcount").innerHTML = '180 / ' + document.getElementById("createfilepath").value.length;

}
function CountCharsDesc() {
    document.getElementById("desccount").innerHTML = '150 . ' + document.getElementById("createdescription").value.length;

}
