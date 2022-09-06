function Edit(obj) {
    let url = '@Url.Action("EditById")';
    let id = obj.id;
    $('#edit-content').load(url, { Id: id });
}

function Delete(obj) {
    let url = '@Url.Action("DeleteById")';
    let id = obj.id;
    $('#edit-content').load(url, { Id: id });
}