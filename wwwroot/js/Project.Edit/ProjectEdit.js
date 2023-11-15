function validation() {

    var projectName = document.getElementById("projecteditformname").value;

    if (projectName.length < 3 || projectName.length > 100) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            type: "error",
            timer: 5000,
            showConfirmButton: false
        });

        return false;
    } else {
        sweetAlert({
            title: "Done!",
            text: "Project Updated",
            type: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}
function CountCharsEditProject() {
    document.getElementById("editprojectnc").innerHTML = '100 / ' + document.getElementById("projecteditformname").value.length;
}


document.getElementById("projecteditformname").onkeyup = function () { return CountCharsEditProject(); };

document.getElementById("projecteditform").onsubmit = function () { return validation(); };