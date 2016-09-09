using FlightCompanion.DAL;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace FlightCompanion
{
    public class OperationProcessor
    {
        public static byte[] GetFlightPlan(string filename)
        {
            return File.ReadAllBytes(filename);
        }

        internal static byte[] GetFlightPlan(int chartType, int selectedChart, HttpContextBase httpContext)
        {
            var chartCategory = new FlightCompanionEntities().ChartTypes.Find(chartType);
            var chart = new FlightCompanionEntities().Charts.Find(selectedChart);

            return File.ReadAllBytes(chart.FileName);
        }        
    }
}