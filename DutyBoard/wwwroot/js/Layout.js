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
//Menu toggle
//let navigation = document.querySelector('.navigation');
//let toggle = document.querySelector('.toggle');
//let main = document.querySelector('.main');
//toggle.onclick = function () {
//    navigation.classList.toggle('active');
//    main.classList.toggle('active');
//}