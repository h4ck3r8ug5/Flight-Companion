using FlightCompanion.DAL;
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

            var chartTypes = DataConnector.GetChartTypes();
            chartTypes.Insert(0, new SelectListItem { Value = "-1", Text = "-Chart Type-" });
            ViewData["ChartTypes"] = chartTypes;
            return View();
        }

        [HttpGet]
        public JsonResult GetSummary(Models.FlightPlan plan)
        {
            var chartTypes = DataConnector.GetChartTypes();
            var response = OperationProcessor.GetFlightPlanSummary(plan);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCharts(int chartType)
        {
            var charts = OperationProcessor.GetCharts(chartType);
            return Json(charts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetChart(int chart)
        {
            var selectedChart = OperationProcessor.GetFlightPlan(chart, HttpContext);
            return new FileContentResult(selectedChart, "application/pdf");
        }

        [HttpGet]
        public ActionResult ViewPlan(FlightPlan plan)
        {
            Response.AppendHeader("Content-Disposition", "inline; filename=FATW_AERODROME CHART_AD-01 _04 FEB 2016.pdf");
            return new FileContentResult(OperationProcessor.GetFlightPlan(Server.MapPath("~/App_Data/FATW_AERODROME CHART_AD-01 _04 FEB 2016.pdf")), "application/pdf");
        }        
    }
}