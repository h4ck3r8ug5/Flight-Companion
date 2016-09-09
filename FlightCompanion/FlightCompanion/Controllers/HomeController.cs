using FlightCompanion.DAL;
using FlightCompanion.Models;
using System.Net.Mime;
using System.Web.Mvc;

namespace FlightCompanion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var icaoCodes = DataConnector.GetICAOCodes();
            icaoCodes.Insert(0, new SelectListItem { Value = "-1", Text = "-ICAO-" });
            ViewData["IcaoCodes"] = icaoCodes;

            return View();
        }

        [HttpGet]
        public JsonResult GetSummary(FlightPlan plan)
        {
            var chartType = DataConnector.GetChartTypes();

            var response = new
            {
                DepartureAirport = "Cape Town",
                DestinationAirport = "O.R Tambo",
                Distance = "945",
                Metar = "123455555 9999KT RBK 21/23 DP 23/43",
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetChart(int chartType, int selectedChart)
        {
            var chart = OperationProcessor.GetFlightPlan(chartType, selectedChart, HttpContext);
            return new FileContentResult(chart, "application/pdf");
        }

        [HttpGet]
        public ActionResult ViewPlan(FlightPlan plan)
        {
            Response.AppendHeader("Content-Disposition", "inline; filename=FATW_AERODROME CHART_AD-01 _04 FEB 2016.pdf");
            return new FileContentResult(OperationProcessor.GetFlightPlan(Server.MapPath("~/App_Data/FATW_AERODROME CHART_AD-01 _04 FEB 2016.pdf")), "application/pdf");
        }
    }
}