

function validation() {
    var projectName = document.getElementById("createprojectformname").value;

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
            text: "Project Added",
            icon: "success",
            timer: 4000,
            showConfirmButton: false
        });
        return true;
    }
}


function CountCharsProject() {
    document.getElementById("project_name_count").innerHTML = '100 / ' + document.getElementById("createprojectformname").value.length;
}