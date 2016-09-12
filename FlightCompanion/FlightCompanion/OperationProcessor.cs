using FlightCompanion.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightCompanion
{
    public class OperationProcessor
    {
        internal static byte[] GetFlightPlan(string filename)
        {
            return File.ReadAllBytes(filename);
        }

        internal static FlightPlanSummary GetFlightPlanSummary(Models.FlightPlan plan)
        {
            var flightPlan = new FlightCompanionEntities().FlightPlans.FirstOrDefault(x => x.Departure == plan.DepartureIcao && x.Destination == plan.DestinationIcao);
            var metar = "123455555 9999KT RBK 21/23 DP 23/43";
            return new FlightPlanSummary
            {
                DepartureAirport = flightPlan.DepartureAirport.AirportName,
                DestinationAirport = flightPlan.DestinationAirport.AirportName,
                Distance = flightPlan.Distance,
                Metar = metar,
                WayPoints = flightPlan.Waypoints.Replace("|"," => "),
            };
        }

        internal static byte[] GetFlightPlan(int chart, HttpContextBase httpContext)
        {
            var selectedChart = new FlightCompanionEntities().Charts.Find(chart);
            var path = httpContext.Server.MapPath(selectedChart.Path + '/' + selectedChart.Name);
            return File.ReadAllBytes(path);
        }

        internal static List<SelectListItem> GetChartTypes()
        {
            var chartTypes = DataConnector.GetChartTypes();
            chartTypes.Insert(0, new SelectListItem { Value = "-1", Text = "-Chart Type-" });
            return chartTypes;
        }

        internal static bool UploadChart(int chartType, HttpPostedFileBase[] files, HttpContextBase httpContext)
        {
            var chartTypes = DataConnector.GetChartTypes();
            foreach (var file in files)
            {
                file.SaveAs(httpContext.Server.MapPath("~/App_Data/" + Path.GetFileName(file.FileName)));
                using (var entities = new FlightCompanionEntities())
                {
                    entities.Charts.Add(new Chart
                    {
                        ChartType = chartType,
                        Name = Path.GetFileName(file.FileName),
                        Path = "~/App_Data"
                    });

                    entities.SaveChanges();
                }
            }

            return true;

        }

        internal static List<SelectListItem> GetCharts(int chartType)
        {
            var charts = new FlightCompanionEntities()
                        .Charts.Where(x => x.ChartType == chartType)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = x.Name
                        }).ToList();

            charts.Insert(0, new SelectListItem { Value = "-1", Text = "-Select-" });
            charts.Reverse();
            return charts;
        }
    }

    public class FlightPlanSummary
    {
        public string Metar { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string Distance { get; set; }
        public string WayPoints { get; internal set; }
    }
}