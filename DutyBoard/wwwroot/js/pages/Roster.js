function Edit(obj) {
    let url = '@Url.Action("EditById", "Roster")';
    let id = obj.id;
    $('#edit-content').load(url, { Id: id });
}

function Delete(obj) {
    let url = '@Url.Action("DeleteById", "Roster")';
    let id = obj.id;
    $('#edit-content').load(url, { Id: id });
}