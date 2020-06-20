using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    class Rwy : INASRDataParser
    {
        public string SiteNumber { get; private set; }
        public string RwyIdentification { get; private set; }
        public int LengthInFeet { get; private set; }
        public int WidthInFeet { get; private set; }
        public string BaseEndIdentifier { get; private set; }
        public int BaseEndTrueAlignment { get; private set; }
        public bool BaseEndRightTraffic { get; private set; }
        public decimal BaseEndLatitude { get; private set; }
        public decimal BaseEndLongitude { get; private set; }
        public double BaseEndElevation { get; private set; }
        public decimal BaseEndDisplacedThresholdLatitude { get; private set; }
        public decimal BaseEndDisplacedThresholdLongitude { get; private set; }
        public double BaseEndDisplacedThresholdElevation { get; private set; }
        public double BaseEndTouchdownZoneElevation { get; private set; }
        public string ReciprocalEndIdentifier { get; private set; }
        public int ReciprocalEndTrueAlignment { get; private set; }
        public bool ReciprocalEndRightTraffic { get; private set; }
        public decimal ReciprocalEndLatitude { get; private set; }
        public decimal ReciprocalEndLongitude { get; private set; }
        public double ReciprocalEndElevation { get; private set; }
        public decimal ReciprocalEndDisplacedThresholdLatitude { get; private set; }
        public decimal ReciprocalEndDisplacedThresholdLongitude { get; private set; }
        public double ReciprocalEndDisplacedThresholdElevation { get; private set; }
        public double ReciprocalEndTouchdownZoneElevation { get; private set; }

        public static bool TryParse(string recordString, out Rwy rwy)
        {
            return NASRDataParserGeneric<Rwy>.TryParse(recordString, 1529, "RWY", fieldList, out rwy);
        }

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (3, 11, BuiltinTypeParsers.ParseString, nameof(SiteNumber), false),
            (16, 7, BuiltinTypeParsers.ParseString, nameof(RwyIdentification), false),
            (23, 5, BuiltinTypeParsers.ParseInt, nameof(LengthInFeet), false),
            (28, 4, BuiltinTypeParsers.ParseInt, nameof(WidthInFeet), false),
            (65, 3, BuiltinTypeParsers.ParseString, nameof(BaseEndIdentifier), false),
            (68, 3, BuiltinTypeParsers.ParseInt, nameof(BaseEndTrueAlignment), false),
            (81, 1, BuiltinTypeParsers.ParseBool, nameof(BaseEndRightTraffic), false),
            (88, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndLatitude), false),
            (115, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndLongitude), false),
            (142, 7, BuiltinTypeParsers.ParseDouble, nameof(BaseEndElevation), false),
            (156, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndDisplacedThresholdLatitude), false),
            (183, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndDisplacedThresholdLongitude), false),
            (210, 7, BuiltinTypeParsers.ParseDouble, nameof(BaseEndDisplacedThresholdElevation), false),
            (221, 7, BuiltinTypeParsers.ParseDouble, nameof(BaseEndTouchdownZoneElevation), false),
            (287, 3, BuiltinTypeParsers.ParseString, nameof(ReciprocalEndIdentifier), false),
            (290, 3, BuiltinTypeParsers.ParseInt, nameof(ReciprocalEndTrueAlignment), false),
            (303, 1, BuiltinTypeParsers.ParseBool, nameof(ReciprocalEndRightTraffic), false),
            (310, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndLatitude), false),
            (337, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndLongitude), false),
            (364, 7, BuiltinTypeParsers.ParseDouble, nameof(ReciprocalEndElevation), false),
            (378, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndDisplacedThresholdLatitude), false),
            (405, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndDisplacedThresholdLongitude), false),
            (432, 7, BuiltinTypeParsers.ParseDouble, nameof(ReciprocalEndDisplacedThresholdElevation), false),
            (443, 7, BuiltinTypeParsers.ParseDouble, nameof(ReciprocalEndTouchdownZoneElevation), false),
        };


        public bool Validate() => true;
    }
}
