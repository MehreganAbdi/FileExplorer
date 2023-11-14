
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

function deleteConfirm(bothpath) {
   

    sweetAlert({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((result) => {
        if (result.isConfirmed) {
            var xhr = new XMLHttpRequest();

            xhr.responseType = 'blob';

            let url = "https://localhost:7242/Home/Delete?bothpath=" + bothpath;

            xhr.open("GET", url, true);
            xhr.onreadystatechange = function () {
                if (this.response != null) {

                    if (this.status == 200) {

                        return response;

                    } else {

                        sweetAlert({
                            title: "Some Error Occured",
                            text: "Try Again",
                            icon: "error",
                            type: "error",
                            showCancelButton: false,
                            timer: 4000
                        });

                    }


                }
            }


            xhr.send();

        } else {
            return false;
        }

    });
    

xhr.onreadystatechange = function () {
    if (this.response != null) {

        if (this.status == 200) {

            return response;

        } else {

            sweetAlert({
                title: "Some Error Occured",
                text: "Try Again",
                icon: "error",
                type: "error",
                showCancelButton: false,
                timer: 4000
            });

        }


    }
}


xhr.send();


    
}


const alldownloadbuttons = document.querySelectorAll('.home-index');

for (const item of alldownloadbuttons) {
    item.addEventListener("click", function () { PathExists(item.getAttribute("data-path"), item.getAttribute("data-type")) });

}
const allDeleteButtons = document.querySelectorAll('.home-index-delete');
for (const delbtn of allDeleteButtons) {
    delbtn.addEventListener("click", deleteConfirm(delbtn.getAttribute("data-bothpath")));
}



