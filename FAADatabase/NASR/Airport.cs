using System;

using FAADataParser.NASR.Apt;

using SQLite;

namespace FAADatabase.NASR
{
    internal class Airport
    {
        [PrimaryKey]
        public string FAAComputerIdentifier { get; set; }
        public LandingFacilityType LandingFacilityType { get; set; }
        [MaxLength(4)]
        public string LocationIdentifier { get; set; }
        public DateTime InformationEffectiveDate { get; set; }
        public string FacilityName { get; set; }
        public AirportOwnershipType AirportOwnershipType { get; set; }
        public FacilityUse FacilityUse { get; set; }
        public float FloatLatitude { get => (float)Latitude; set => Latitude = (decimal)value; }
        public float FloatLongitude { get => (float)Longitude; set => Longitude = (decimal)value; }
        public float FloatElevation { get => (float)Elevation; set => Elevation = (decimal)value; }
        public int? MagneticVariation { get; set; }
        public int? MagneticVariationEpochYear { get; set; }
        public int? TPA { get; set; }
        [Ignore]
        public decimal Latitude { get; set; }
        [Ignore]
        public decimal Longitude { get; set; }
        [Ignore]
        public decimal Elevation { get; set; }
    }
}
