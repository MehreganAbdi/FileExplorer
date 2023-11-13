function validation() {

    var filePath = document.getElementById("fileentitycreatefromfilepath").value;
    var desc = document.getElementById("fileentitycreatefromdesc").value;
    var file = document.getElementById("fileentitycreatefromfile").value;


    if (desc.length > 180) {
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


function CountCharsFilePath() {
    document.getElementById("filepath_count").innerHTML = '180 / ' + document.getElementById("fileentitycreateformfilepath").value != null?document.getElementById("fileentitycreateformfilepath").value.length:0;

}
function CountCharsDesc() {
    document.getElementById("desc_count").innerHTML = '150 / ' + document.getElementById("fileentitycreateformdesc").value != null ? document.getElementById("fileentitycreateformdesc").value.length : 0 ;

}