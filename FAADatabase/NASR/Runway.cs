using SQLite;

namespace FAADatabase.NASR
{
    internal class Runway
    {
        public string AirportPrimaryKey { get; set; }
        public string Identification { get; set; }
        public int LengthInFeet { get; set; }
        public int WidthInFeet { get; set; }
        public string BaseEndIdentifier { get; set; }
        public int? BaseEndTrueAlignment { get; set; }
        public bool BaseEndRightTraffic { get; set; }
        public float? FloatBaseEndLatitude { get => (float?)BaseEndLatitude; set => BaseEndLatitude = (decimal?)value; }
        public float? FloatBaseEndLongitude { get => (float?)BaseEndLongitude; set => BaseEndLongitude = (decimal?)value; }
        [Ignore]
        public decimal? BaseEndLatitude { get; set; }
        [Ignore]
        public decimal? BaseEndLongitude { get; set; }
        public double? BaseEndElevation { get; set; }
        public float? FloatBaseEndDisplacedThresholdLatitude { get => (float?)BaseEndDisplacedThresholdLatitude; set => BaseEndDisplacedThresholdLatitude = (decimal?)value; }
        public float? FloatBaseEndDisplacedThresholdLongitude { get => (float?)BaseEndDisplacedThresholdLongitude; set => BaseEndDisplacedThresholdLongitude = (decimal?)value; }
        [Ignore]
        public decimal? BaseEndDisplacedThresholdLatitude { get; set; }
        [Ignore]
        public decimal? BaseEndDisplacedThresholdLongitude { get; set; }
        public double? BaseEndDisplacedThresholdElevation { get; set; }
        public double? BaseEndTouchdownZoneElevation { get; set; }
        public string ReciprocalEndIdentifier { get; set; }
        public int? ReciprocalEndTrueAlignment { get; set; }
        public bool ReciprocalEndRightTraffic { get; set; }
        public float? FloatReciprocalEndLatitude { get => (float?)ReciprocalEndLatitude; set => ReciprocalEndLatitude = (decimal?)value; }
        public float? FloatReciprocalEndLongitude { get => (float?)ReciprocalEndLongitude; set => ReciprocalEndLongitude = (decimal?)value; }
        [Ignore]
        public decimal? ReciprocalEndLatitude { get; set; }
        [Ignore]
        public decimal? ReciprocalEndLongitude { get; set; }
        public double? ReciprocalEndElevation { get; set; }
        public float? FloatReciprocalEndDisplacedThresholdLatitude { get => (float?)ReciprocalEndDisplacedThresholdLatitude; set => ReciprocalEndDisplacedThresholdLatitude = (decimal?)value; }
        public float? FloatReciprocalEndDisplacedThresholdLongitude { get => (float?)ReciprocalEndDisplacedThresholdLongitude; set => ReciprocalEndDisplacedThresholdLongitude = (decimal?)value; }
        [Ignore]
        public decimal? ReciprocalEndDisplacedThresholdLatitude { get; set; }
        [Ignore]
        public decimal? ReciprocalEndDisplacedThresholdLongitude { get; set; }
        public double? ReciprocalEndDisplacedThresholdElevation { get; set; }
        public double? ReciprocalEndTouchdownZoneElevation { get; set; }
    }
}
