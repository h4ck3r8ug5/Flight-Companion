$(document).ready(function () {
    $("#btnSubmitFrmRoutePlan").on('click', function () {
        $.get('/home/getSummary?departureIcao=' + $("#departureIcao").val() + '&destinationIcao=' + $("#destinationIcao").val(), function (data) {
            $("#departureAirport").text(data.DepartureAirport);
            $("#destinationAirport").text(data.DestinationAirport);
            $("#distance").text(data.Distance);
            $("#metar").text(data.Metar);
            $("#chartType").removeClass("disabled");
        });
        
    });

    $("#chartType").change(function () {
        $.get('/home/getCharts?chartType=' + $(this).val(), function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#selectedChart").prepend(new Option(data[i].Text, data[i].Value));
            }
            $("#selectedChart").removeClass("disabled");
        });
    });

    $("#selectedChart").change(function () {
        $.get('/home/getChart?chart=' + $(this).val(), function (data) {
            $("#flightPlanPdf").attr('src', '/home/viewPlan?departureIcao=' + $("#departureIcao").val() + '&destinationIcao=' + $("#destinationIcao").val());
        });
    });
});