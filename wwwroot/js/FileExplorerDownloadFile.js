
async function PathExists(path) {
    import * as PathData from path;

    File.PathExists(path);
    if (PathData != null) {
        document.write('<a class="btn-primary" asp-action="DownloadView" asp-route-path="@item.path" asp-controller="Home" asp-route-type="@item.Type" asp-route-directory="@Model.path">Download File</a>');
    } 

    document.write('Didn"t Exist , Reload');
}
    
