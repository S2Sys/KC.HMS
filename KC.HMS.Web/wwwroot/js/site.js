// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.datepicker').datepicker(
        {
            minDate: 1,
            maxDate: "+91D",
            changeMonth: true,
            changeYear: true,
            todayHighlight: true,
            dateFormat: 'dd/mm/yy'
        }
    );

    LoadDashboard();
    setRoomsSelected();

    $(".checkRoom").change(function () {
        setRoomsSelected();
    });

    $(".btnBookMyRoom").click(function () {

        setRoomsSelected();
    });
});



function setRoomsSelected() {
    var selected = '';
    var boxes = $('input[name=checkRoom]:checked');

    boxes.each(function () {
        selected = selected + $(this).val() + ';';
    });

    if ($('.btnBookMyRoom').length == 1)
        $('.RoomsSelected').val(selected);

    var roomsSelected = boxes.length;
    var roomCount = parseInt($('.RoomCount').val());
    var btn = $('.btnBookMyRoom')
    if (roomsSelected == roomCount) {
        btn.removeClass('disabled');

    }
    else {
        btn.addClass('disabled');
    }
}


function LoadDashboard() {
    $('#calendar').fullCalendar({
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'today',
            month: 'month',
            week: 'week',
            day: 'day'
        },

        events: function (start, end, timezone, callback) {

            //$.get('/DashboardData/').done(function (datas) {
            //    var events = [];
            //    $.each(datas, function (i, data) {
            //        var item = `<li>
            //                <strong>${car.make} ${car.model}</strong>
            //                (£${car.price})</li>`;
            //        $('#car-list').append(item);
            //    });
            //});

            $.ajax({
                url: '/DashboardData',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                title: data.roomNumber + " (C) " + data.totalCost ,
                                description: data.customerName + " (P) " + data.customerPhone,
                                start: moment(data.checkIn),//.format('YYYY-MM-DD'),
                                end: moment(data.checkOut),//.format('YYYY-MM-DD'),
                                backgroundColor: "#2C3D98",
                                borderColor: "#2C3D98"
                            });
                    });

                    callback(events);
                }
            });
        },

        eventRender: function (event, element) {
            element.qtip(
                {
                    content: event.description
                });
        },

        editable: false
    });
}

