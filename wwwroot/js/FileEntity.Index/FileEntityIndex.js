function deleteConfirm(id) {

    let url = "https://localhost:7242/FileEntity/Delete/" + id;

    sweetAlert({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover\n",
        type: "warning",
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: "Yes, Delete It",
        cancelButtonText: "No"
    }).then(async function (result) {
        if (result.dismiss != 'cancel') {
            const response = await fetch(url, {
                method: 'GET'

            });

            if (response.ok) {
                sweetAlert({
                    title: "Done!",
                    text: "Record Deleted Successfully",
                    type: "success"
                });
            } else {
                sweetAlert("Error Occured", "Record Didn't Exist Or An Error Occurred", "error")
            }


        } else {
            sweetAlert("Proccess Canceled ", "Record Is Safe", "success");
        }

    });
}

const allDeleteButtons = document.querySelectorAll('.file-index-delete');
for (const delbtn of allDeleteButtons) {
    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-id")); });
}
    
 