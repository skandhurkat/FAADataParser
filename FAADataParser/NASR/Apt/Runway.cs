using System.Collections.Generic;

namespace FAADataParser.NASR.Apt
{
    public class Runway : ILandingStructure
    {
        public string Identification { get; set; }
        public int LengthInFeet { get; set; }
        public int WidthInFeet { get; set; }
        public List<RunwaySurfaceType> SurfaceType { get; set; }
        public RunwaySurfaceCondition? SurfaceCondition { get; set; }
        public string BaseEndIdentifier { get; set; }
        public int? BaseEndTrueAlignment { get; set; }
        public bool BaseEndRightTraffic { get; set; }
        public decimal? BaseEndLatitude { get; set; }
        public decimal? BaseEndLongitude { get; set; }
        public double? BaseEndElevation { get; set; }
        public decimal? BaseEndDisplacedThresholdLatitude { get; set; }
        public decimal? BaseEndDisplacedThresholdLongitude { get; set; }
        public double? BaseEndDisplacedThresholdElevation { get; set; }
        public double? BaseEndTouchdownZoneElevation { get; set; }
        public string ReciprocalEndIdentifier { get; set; }
        public int? ReciprocalEndTrueAlignment { get; set; }
        public bool ReciprocalEndRightTraffic { get; set; }
        public decimal? ReciprocalEndLatitude { get; set; }
        public decimal? ReciprocalEndLongitude { get; set; }
        public double? ReciprocalEndElevation { get; set; }
        public decimal? ReciprocalEndDisplacedThresholdLatitude { get; set; }
        public decimal? ReciprocalEndDisplacedThresholdLongitude { get; set; }
        public double? ReciprocalEndDisplacedThresholdElevation { get; set; }
        public double? ReciprocalEndTouchdownZoneElevation { get; set; }
    }
}
