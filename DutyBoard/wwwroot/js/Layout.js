$(document).ready(function () {
    var str = window.location.pathname.split('/')[1]
    var res = '#' + (!isEmpty(str) ? str : 'Home');
    $(res).addClass('active');
});
function isEmpty(str) {
    if (str.trim() == '')
        return true;
    return false;
}