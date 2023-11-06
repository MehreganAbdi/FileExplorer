
async function PathExists(path,type) {

    var xhr = new XMLHttpRequest();

    xhr.responseType = 'blob';

    let url = "https://localhost:7242/Home/Download?path=" + path+"&type="+type;
    
    xhr.open("GET", url, true);


    xhr.onreadystatechange = function () {
        if (this.response != null) {

        if (  this.status == 200)
        {

            saveBlob(xhr.response,"FileData");

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