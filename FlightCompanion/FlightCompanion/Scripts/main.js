$(document).ready(function () {
    $("#btnSubmitFrmRoutePlan").on('click', function () {
        $.get('/home/getSummary?departureIcao=' + $("#departureIcao").val() + '&destinationIcao=' + $("#destinationIcao").val(), function (data) {
            $("#departureAirport").text(data.DepartureAirport);
            $("#destinationAirport").text(data.DestinationAirport);
            $("#distance").text(data.Distance);
            $("#metar").text(data.Metar);
        });
        $("#flightPlanPdf").attr('src', '/home/viewPlan?departureIcao=' + $("#departureIcao").val() + '&destinationIcao=' + $("#destinationIcao").val());
    });
});