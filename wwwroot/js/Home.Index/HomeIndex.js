
async function PathExists(path, type) {

    var xhr = new XMLHttpRequest();

    xhr.responseType = 'blob';

    let url = "https://localhost:7242/Home/Download?path=" + path + "&type=" + type;

    xhr.open("GET", url, true);


    xhr.onreadystatechange = function () {
        if (this.response != null) {

            if (this.status == 200) {

                saveBlob(xhr.response, "FileData." + type);

            } else {

                alert("Item Is Not Available");

            }

        }
    }


    xhr.send();
}

function saveBlob(blob, fileName) {
    var a = document.createElement('a');
    a.href = window.URL.createObjectURL(blob);
    a.download = fileName;
    a.dispatchEvent(new MouseEvent('click'));
}

function deleteConfirm(path) {

    let url = "https://localhost:7242/Home/Delete?path=" + path;

    sweetAlert({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover\n",
        type: "warning",
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: "Yes, Delete It",
        cancelButtonText : "No"
    }).then(async function(result) {
        if (result.dismiss != 'cancel') {
            const response = await fetch(url, {
                method: 'GET'

            });

            if (response.ok) {
                sweetAlert({
                    title: "Done!",
                    text: "File Deleted Successfully",
                    type: "success"
                });
            } else {
                sweetAlert("Error Occured" , "Try Again Later" , "error" )
            }


        } else {
            sweetAlert("Proccess Canceled ", "File Is Safe", "success");
        }

    });
}


document.getElementById("selectfilehomepageinput").onchange = function () {

    document.getElementById("selectfilehomepagelabel").innerHTML = "Selected";
    document.getElementById("selectfilehomepagesubmit").type = "submit";
}



const alldownloadbuttons = document.querySelectorAll('.home-index');
for (const item of alldownloadbuttons) {
    item.addEventListener("click", function () { PathExists(item.getAttribute("data-path"), item.getAttribute("data-type")) });

}
const allDeleteButtons = document.querySelectorAll('.home-index-delete');
for (const delbtn of allDeleteButtons) {
    delbtn.addEventListener("click", function () { deleteConfirm(delbtn.getAttribute("data-path")); });
}



