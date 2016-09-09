using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FlightCompanion.DAL
{
    public class DataConnector
    {
        public static List<SelectListItem> GetICAOCodes()
        {
            using (var dbContext = new FlightCompanionEntities())
            {
                return dbContext.IcaoCodes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code }).ToList();
            }
        }

        public static List<Aircraft> GetAircraft()
        {
            using (var dbContext = new FlightCompanionEntities())
            {
                return dbContext.Aircraft.ToList();
            }
        }

        internal static List<SelectListItem> GetChartTypes()
        {
            using (var dbContext = new FlightCompanionEntities())
            {
                return dbContext.ChartTypes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Value }).ToList();
            }
        }

        public static List<FlightHistory> GetFlightHistory()
        {
            using (var dbContext = new FlightCompanionEntities())
            {
                return dbContext.FlightHistories.ToList();
            }
        }
    }
}