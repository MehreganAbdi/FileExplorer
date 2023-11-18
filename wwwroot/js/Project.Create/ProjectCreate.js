
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
document.getElementById("createprojectformname").onkeyup = function () { return CountCharsProject() }; 

document.getElementById("createpeojectsubmit").onclick = function () {
    if (validation()) {

        let url = "https://localhost:7242/Project/Create/"

        var valdata = $("#createprojectform").serialize();

        sweetAlert({
            title: "Are you sure?",
            text: "",
            type: "warning",
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: "Yes, Submit It",
            cancelButtonText: "No",
            
        }).then(async function (result) {
            if (result.dismiss != 'cancel') {

                const response = await fetch(url, {
                    method: 'POST',
                    body: JSON.stringify(valdata),
                    headers: {
                        "Content-type": "application/json; charset=UTF-8"
                    }
                })

                if (response.ok) {
                    sweetAlert({
                        title: "Done!",
                        text: "Form Submitted Successfully",
                        type: "success"
                    });
                } else {
                    sweetAlert("Error Occured", "Record Didn't Exist Or An Error Occurred", "error")
                }
            }
            else {
                sweetAlert("Canceled", "", "success")

            }


        })


    } else {
        return validation();
    }
}







