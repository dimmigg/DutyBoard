
$("#fromDate").inputmask({
    mask: "99.99.9999"
});
$("#toDate").inputmask({
    mask: "99.99.9999"
});
$(function () {
    var from = $('#fromDate')
        .datepicker(
            {
                closeText: 'Закрыть',
                prevText: '',
                currentText: 'Сегодня',
                monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
                    'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
                monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
                    'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
                dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
                dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
                dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
                weekHeader: 'Не',
                dateFormat: "dd.mm.yy",
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: '',
                showAnim: "slideDown"
            }
        )
        .on("change", function () {
            to.datepicker("option", "minDate", getDate(this));
        }),

        to = $("#toDate").datepicker({
            closeText: 'Закрыть',
            prevText: '',
            currentText: 'Сегодня',
            monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
                'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
                'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
            dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
            dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
            dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
            weekHeader: 'Не',
            dateFormat: "dd.mm.yy",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: '',
            showAnim: "slideDown"
        })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            });

    function getDate(element) {
        var date;
        var dateFormat = "dd.mm.yy";
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }
        return date;
    }
})