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
function getDate(dt) {
    var year = dt.getFullYear();
    var month = '';
    var month1 = dt.getMonth() + 1;
    if (month1 < 10) {
        month = '0' + month1;
    } else {
        month = month1;
    }

    var day = '';
    if (dt.getDate() < 10) {
        day = '0' + dt.getDate();
    }
    else {
        day = dt.getDate();
    }
    return day + "." + month + "." + year;
}

var dt = (new Date(new Date().setDate(1)));
dt.setMonth(dt.getMonth() + 1);
document.getElementById("fromDate").value = getDate(dt);
dt.setMonth(dt.getMonth() + 1);
dt.setDate(dt.getDate() - 1);
document.getElementById("toDate").value = getDate(dt)


getCalendarRange('#fromDate', '#toDate');
function Calc() {
    let url = '/Home/ConfCalc/';
    let fromDt = $('#fromDate').val();
    let toDt = $('#toDate').val();
    $('#edit-content').load(url, { fromDate: fromDt, toDate: toDt });
}
