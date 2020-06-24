using System.Collections.Generic;
using System.Text.RegularExpressions;
using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    public enum RunwaySurfaceType
    {
        Concrete,
        Asphalt,
        Snow,
        Ice,
        Mats,
        Wood,
        Treated,
        Gravel,
        Turf,
        Dirt,
        Water
    };
    public enum RunwaySurfaceCondition
    {
        Excellent,
        Good,
        Fair,
        Poor,
        Failed
    }
    class Rwy : INASRDataParser
    {
        public string SiteNumber { get; private set; }
        public string RwyIdentification { get; private set; }
        public int LengthInFeet { get; private set; }
        public int WidthInFeet { get; private set; }
        public (List<RunwaySurfaceType> surfaceTypes, RunwaySurfaceCondition? surfaceCondition) RunwaySurface { get; private set; }
        public string BaseEndIdentifier { get; private set; }
        public int? BaseEndTrueAlignment { get; private set; }
        public bool? BaseEndRightTraffic { get; private set; }
        public decimal? BaseEndLatitude { get; private set; }
        public decimal? BaseEndLongitude { get; private set; }
        public double? BaseEndElevation { get; private set; }
        public decimal? BaseEndDisplacedThresholdLatitude { get; private set; }
        public decimal? BaseEndDisplacedThresholdLongitude { get; private set; }
        public double? BaseEndDisplacedThresholdElevation { get; private set; }
        public double? BaseEndTouchdownZoneElevation { get; private set; }
        public string ReciprocalEndIdentifier { get; private set; }
        public int? ReciprocalEndTrueAlignment { get; private set; }
        public bool? ReciprocalEndRightTraffic { get; private set; }
        public decimal? ReciprocalEndLatitude { get; private set; }
        public decimal? ReciprocalEndLongitude { get; private set; }
        public double? ReciprocalEndElevation { get; private set; }
        public decimal? ReciprocalEndDisplacedThresholdLatitude { get; private set; }
        public decimal? ReciprocalEndDisplacedThresholdLongitude { get; private set; }
        public double? ReciprocalEndDisplacedThresholdElevation { get; private set; }
        public double? ReciprocalEndTouchdownZoneElevation { get; private set; }

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
            (32, 12, ParseRunwaySurface, nameof(RunwaySurface), false),
            (65, 3, BuiltinTypeParsers.ParseString, nameof(BaseEndIdentifier), false),
            (68, 3, BuiltinTypeParsers.ParseInt, nameof(BaseEndTrueAlignment), true),
            (81, 1, BuiltinTypeParsers.ParseBool, nameof(BaseEndRightTraffic), true),
            (88, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndLatitude), true),
            (115, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndLongitude), true),
            (142, 7, BuiltinTypeParsers.ParseDouble, nameof(BaseEndElevation), true),
            (156, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndDisplacedThresholdLatitude), true),
            (183, 15, ParseLatitudeLongitude.ParseLatLong, nameof(BaseEndDisplacedThresholdLongitude), true),
            (210, 7, BuiltinTypeParsers.ParseDouble, nameof(BaseEndDisplacedThresholdElevation), true),
            (221, 7, BuiltinTypeParsers.ParseDouble, nameof(BaseEndTouchdownZoneElevation), true),
            (287, 3, BuiltinTypeParsers.ParseString, nameof(ReciprocalEndIdentifier), true),
            (290, 3, BuiltinTypeParsers.ParseInt, nameof(ReciprocalEndTrueAlignment), true),
            (303, 1, BuiltinTypeParsers.ParseBool, nameof(ReciprocalEndRightTraffic), true),
            (310, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndLatitude), true),
            (337, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndLongitude), true),
            (364, 7, BuiltinTypeParsers.ParseDouble, nameof(ReciprocalEndElevation), true),
            (378, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndDisplacedThresholdLatitude), true),
            (405, 15, ParseLatitudeLongitude.ParseLatLong, nameof(ReciprocalEndDisplacedThresholdLongitude), true),
            (432, 7, BuiltinTypeParsers.ParseDouble, nameof(ReciprocalEndDisplacedThresholdElevation), true),
            (443, 7, BuiltinTypeParsers.ParseDouble, nameof(ReciprocalEndTouchdownZoneElevation), true),
        };

        public bool Validate()
        {
            if (RunwaySurface.surfaceTypes.Count > 2)
            {
                return false;
            }
            if (instrumentRunwayBogusEntry.IsMatch(RwyIdentification))
            {
                // Skip these entries, haven't figured out how to handle these yet
                return true;
            }
            if (helipadRegex.IsMatch(RwyIdentification))
            {
                if (!(ReciprocalEndIdentifier is null)
                    || !(ReciprocalEndTrueAlignment is null)
                    || !(ReciprocalEndRightTraffic is null)
                    || !(ReciprocalEndLatitude is null)
                    || !(ReciprocalEndLongitude is null)
                    || !(ReciprocalEndElevation is null)
                    || !(ReciprocalEndDisplacedThresholdLatitude is null)
                    || !(ReciprocalEndDisplacedThresholdLongitude is null)
                    || !(ReciprocalEndDisplacedThresholdElevation is null)
                    || !(ReciprocalEndTouchdownZoneElevation is null)
                    || !(BaseEndDisplacedThresholdLatitude is null)
                    || !(BaseEndDisplacedThresholdLongitude is null)
                    || !(BaseEndDisplacedThresholdElevation is null)
                    || BaseEndIdentifier != RwyIdentification
                    )
                {
                    return false;
                }
            }
            else if (balloonportRegex.IsMatch(RwyIdentification))
            {
                if (!(ReciprocalEndIdentifier is null)
                    || !(ReciprocalEndTrueAlignment is null)
                    || !(ReciprocalEndRightTraffic is null)
                    || !(ReciprocalEndLatitude is null)
                    || !(ReciprocalEndLongitude is null)
                    || !(ReciprocalEndElevation is null)
                    || !(ReciprocalEndDisplacedThresholdLatitude is null)
                    || !(ReciprocalEndDisplacedThresholdLongitude is null)
                    || !(ReciprocalEndDisplacedThresholdElevation is null)
                    || !(ReciprocalEndTouchdownZoneElevation is null)
                    || !(BaseEndDisplacedThresholdLatitude is null)
                    || !(BaseEndDisplacedThresholdLongitude is null)
                    || !(BaseEndDisplacedThresholdElevation is null)
                    || !(BaseEndTouchdownZoneElevation is null)
                    || BaseEndIdentifier != RwyIdentification
                    )
                {
                    return false;
                }
            }
            else if (runwayRegex.IsMatch(RwyIdentification))
            {
                Match match = runwayRegex.Match(RwyIdentification);
                if ((match.Groups["base"].Value != BaseEndIdentifier
                    || match.Groups["reciprocal"].Value != ReciprocalEndIdentifier)
                    && (match.Groups["base"].Value != ReciprocalEndIdentifier
                    || match.Groups["reciprocal"].Value != BaseEndIdentifier))
                {
                    return false;
                }
                // TODO: There are runway surface combinations that make no sense, e.g. CONC-WATER.
                // Check for these later.
                if (
                    ((BaseEndLatitude is null) ^ (BaseEndLongitude is null))
                    || ((ReciprocalEndLatitude is null) ^ (ReciprocalEndLongitude is null))
                    // There are runways that violate this check :(
                    // || ((BaseEndLatitude is null) && !(ReciprocalEndLatitude is null))
                    // || ((BaseEndLongitude is null) && !(ReciprocalEndLongitude is null))
                    // || ((BaseEndTrueAlignment is null) && !(ReciprocalEndTrueAlignment is null))
                    || ((BaseEndDisplacedThresholdLatitude is null) ^ (BaseEndDisplacedThresholdLongitude is null))
                    || ((ReciprocalEndDisplacedThresholdLatitude is null) ^ (ReciprocalEndDisplacedThresholdLongitude is null))
                    )
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (BaseEndRightTraffic is null)
            {
                BaseEndRightTraffic = false;
            }
            if (ReciprocalEndRightTraffic is null)
            {
                ReciprocalEndRightTraffic = false;
            }
            return true;
        }
        private static bool ParseRunwaySurface(string input, out object output)
        {
            if (string.IsNullOrEmpty(input))
            {
                output = null;
                return false;
            }
            string[] splitInput = input.Split(new char[] { '-', '/' });
            var surfaceType = new List<RunwaySurfaceType>();
            RunwaySurfaceCondition? surfaceCondition = null;
            foreach (string token in splitInput)
            {
                switch (token)
                {
                    case "CONC":
                        surfaceType.Add(RunwaySurfaceType.Concrete);
                        break;
                    case "ASPH":
                        surfaceType.Add(RunwaySurfaceType.Asphalt);
                        break;
                    case "SNOW":
                        surfaceType.Add(RunwaySurfaceType.Snow);
                        break;
                    case "ICE":
                        surfaceType.Add(RunwaySurfaceType.Ice);
                        break;
                    case "MATS":
                        surfaceType.Add(RunwaySurfaceType.Mats);
                        break;
                    case "WOOD":
                        surfaceType.Add(RunwaySurfaceType.Wood);
                        break;
                    case "TRTD":
                    case "TREATED":
                        surfaceType.Add(RunwaySurfaceType.Treated);
                        break;
                    case "GRAVEL":
                    case "GRVL":
                        surfaceType.Add(RunwaySurfaceType.Gravel);
                        break;
                    case "SOD":
                    case "GRASS":
                    case "TURF":
                        surfaceType.Add(RunwaySurfaceType.Turf);
                        break;
                    case "DIRT":
                        surfaceType.Add(RunwaySurfaceType.Dirt);
                        break;
                    case "WATER":
                        surfaceType.Add(RunwaySurfaceType.Water);
                        break;
                    // These are all nonsense entries in the database.
                    case "ROOF":
                    case "TOP":
                    case "ROOFTOP":
                    case "DECK":
                    case "ALUM":
                    case "ALUMINUM":
                    case "PFC":
                    case "PEM":
                    case "PSP":
                    case "STEEL":
                    case "OIL&CHIP":
                    case "T":
                    case "SAND":
                    case "METAL":
                    case "CORAL":
                    case "BRICK":
                    case "NSTD":
                    case "GRE":
                    case "CALICHE":
                        break;
                    // Start parsing surface conditions here.
                    case "E":
                        if (!(surfaceCondition is null))
                        {
                            output = null;
                            return false;
                        }
                        surfaceCondition = RunwaySurfaceCondition.Excellent;
                        break;
                    case "G":
                        if (!(surfaceCondition is null))
                        {
                            output = null;
                            return false;
                        }
                        surfaceCondition = RunwaySurfaceCondition.Good;
                        break;
                    case "F":
                        if (!(surfaceCondition is null))
                        {
                            output = null;
                            return false;
                        }
                        surfaceCondition = RunwaySurfaceCondition.Fair;
                        break;
                    case "P":
                        if (!(surfaceCondition is null))
                        {
                            output = null;
                            return false;
                        }
                        surfaceCondition = RunwaySurfaceCondition.Poor;
                        break;
                    case "L":
                        if (!(surfaceCondition is null))
                        {
                            output = null;
                            return false;
                        }
                        surfaceCondition = RunwaySurfaceCondition.Failed;
                        break;
                    default:
                        output = null;
                        return false;
                }
            }
            output = (surfaceType, surfaceCondition);
            return true;
        }
        public static readonly Regex helipadRegex = new Regex(@"^H-?(?:|\d+|[A-Z])$");
        public static readonly Regex balloonportRegex = new Regex(@"^B\d+$");
        public static readonly Regex runwayRegex = new Regex(@"^(?<base>\d{2,3}[LRCNESWUG]?|[NESW]{1,3}[LRC]?|ALL)/(?<reciprocal>\d{2,3}[LRCNESWUG]?|[NESW]{1,3}[LRC]?|WAY)$");
        /// <summary>
        /// Used to indicate runways that are placeholders for some instrument approach data.
        /// </summary>
        public static readonly Regex instrumentRunwayBogusEntry = new Regex(@"^\d{2}X");
    }
}
