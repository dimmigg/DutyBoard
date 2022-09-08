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

//Menu toggle
//let navigation = document.querySelector('.navigation');
//let toggle = document.querySelector('.toggle');
//let main = document.querySelector('.main');
//toggle.onclick = function () {
//    navigation.classList.toggle('active');
//    main.classList.toggle('active');
//}