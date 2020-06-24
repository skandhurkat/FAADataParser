using System.Collections.Generic;

namespace FAADataParser.NASR.Apt
{
    public class Helipad : ILandingStructure
    {
        public string Identification { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public double? Elevation { get; set; }
        public List<RunwaySurfaceType> SurfaceType { get; set; }
        public RunwaySurfaceCondition? SurfaceCondition { get; set; }
    }
}
