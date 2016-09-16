using System;

namespace FlightCompanion.Models
{
    public class FlightPlan
    {
        public int DepartureIcao { get; set; }
        public int DestinationIcao { get; set; }
        public string Waypoints { get; set; }
        public string Distance { get; set; }
    }
}