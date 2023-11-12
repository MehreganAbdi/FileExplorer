function deleteConfirm() {

    sweetAlert({
        title: "Are You Sure?",
        text: "Deleted Files Are Not Recycable",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Delete!",
        cancelButtonText: "Cancel"
    })


}
