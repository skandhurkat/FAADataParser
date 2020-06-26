using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FAADataParser.NASR.Awos
{
    public class Awos
    {
        public string Identifier { get; set; }
        public WxSensorType SensorType { get; set; }
        public bool CommissioningStatus { get; set; }
        public bool AssociatedWithNavaid { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Elevation { get; set; }
        public decimal? StationFrequency { get; set; }
        public decimal? SecondaryStationFrequency { get; set; }
        [Phone]
        public string StationPhoneNumber { get; set; }
        [Phone]
        public string SecondaryStationPhoneNumber { get; set; }
        public string LandingFacilityComputerIdentifier { get; set; }
        public string StationCity { get; set; }
        public string StationStatePOCode { get; set; }
        public DateTime InformationEffectiveDate { get; set; }
        public List<string> Remarks { get; set; } = new List<string>();
    }
}
