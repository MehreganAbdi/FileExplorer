function validation() {

    var projectName = document.getElementById("projecteditformname").value;

    if (projectName.length < 3 || projectName.length > 100) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            icon: "error",
            timer: 5000,
            showConfirmButton: false
        });

        return false;
    } else {
        sweetAlert({
            title: "Done!",
            text: "Project Updated",
            icon: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}
function CountCharsEditProject() {
    document.getElementById("editporject_name_count").innerHTML = '100 / ' + document.getElementById("projecteditformname").value.length;
}