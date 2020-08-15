using SQLite;

namespace FAADatabase.NASR
{
    internal class Helipad
    {
        public string AirportPrimaryKey { get; set; }
        public string Identification { get; set; }
        public float? FloatLatitude { get => (float?)Latitude; set => Latitude = (decimal?)value; }
        public float? FloatLongitude { get => (float?)Longitude; set => Longitude = (decimal?)value; }
        [Ignore]
        public decimal? Latitude { get; set; }
        [Ignore]
        public decimal? Longitude { get; set; }
        public double? Elevation { get; set; }

    }
}
