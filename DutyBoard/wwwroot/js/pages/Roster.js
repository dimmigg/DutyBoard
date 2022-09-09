    function changeIsAlways(obj) {
        $('#datepicker').prop("hidden", $(obj).is(":checked"));
        if ($(obj).is(":checked")) {
            $('#day').addClass('opacity-25');
            $('#day').removeClass('opacity-100');
            $('#always').addClass('opacity-100');
            $('#always').removeClass('opacity-25');
            $('#date').val(new Date().toLocaleDateString("ru-RU"));
        }
        else {
            $('#day').addClass('opacity-100');
            $('#day').removeClass('opacity-25');
            $('#always').addClass('opacity-25');
            $('#always').removeClass('opacity-100');
        }
        getrosterbyid('#date');
    }

    function getrosterbyid(obj) {
        let val = $('#switchcheck').is(":checked") ? "-1" : $(obj).val();
        $.ajax({
            type: 'GET',
            url: '/Workday/GetRosterByDate/' + val,
            success: function (data) {
                $('#rosters').replaceWith(data);
            }
        });
    }