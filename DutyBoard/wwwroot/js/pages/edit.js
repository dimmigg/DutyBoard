function Edit(obj, controller) {
    let url = '/' + controller + '/EditById/';
    let id = obj.id;
    $('#edit-content').load(url, { Id: id });
}

function Delete(obj, controller) {
    let url = '/' + controller + '/DeleteById/';
    let id = obj.id;
    $('#edit-content').load(url, { Id: id });
}
