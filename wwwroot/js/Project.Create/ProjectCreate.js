
function validation() {
    var projectName = document.getElementById("createprojectformname").value;



    if (projectName.length < 3 || projectName.length > 100) {
        sweetAlert({
            title: "project name must be between 3 and 100 characters",
            text: "",
            type: "error",
            timer: 4000,
            showConfirmButton: false
        });
        return false;
    } else {

        //sweetAlert({
        //    title: "Done!",
        //    text: "Project Added",
        //    type: "success",
        //    timer: 4000,
        //    showConfirmButton: false
        //});
        return true;
    }


}


function CountCharsProject() {
    document.getElementById("projectnamecount").innerHTML = '100 / ' + document.getElementById("createprojectformname").value.length;
}

document.getElementById("createprojectform").onsubmit = function () {
    if (validation()) {
        var valdata = $("#createprojectform").serialize();

        $.ajax({
            url: "/Peoject/Create",
            type: "POST",
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: valdata,
            complete: function (data) {
                if (data) {
                    sweetAlert({
                        title: "Done",
                        text: data,
                        type: "success",
                        timer: 4000

                    });
                } else {
                    sweetAlert({
                        title: "Failed",
                        text: "Couldn't Create The Project",
                        type: "error",
                        timer: 4000

                    });
                }


            }
        })
    }
    else {
        return validation();
    }
};

document.getElementById("createprojectformname").onkeyup = function () { return CountCharsProject() };



