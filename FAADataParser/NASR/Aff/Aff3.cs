using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Aff
{
    public enum AltitudeSector { Low, High, LowHigh, UltraHigh };
    public enum FrequencySpecialUsage { None, ApproachControl, Discrete, DoNotPublish, Oceanic };
    class Aff3 : INASRDataParser
    {
        public string ArtccIdent { get; private set; }
        public string SiteLocation { get; private set; }
        public FacilityType FacilityType { get; private set; }
        public decimal Frequency { get; private set; }
        public AltitudeSector AltitudeSector { get; private set; }
        public FrequencySpecialUsage FrequencySpecialUsage { get; private set; }
        public bool? RCAGFrequencyCharted { get; private set; } = null;
        public string AirportIdent { get; private set; } = null;
        public string AirportState { get; private set; } = null;
        public string AirportStatePOCode { get; private set; } = null;
        public string AirportCity { get; private set; } = null;
        public string AirportName { get; private set; } = null;
        public decimal? AirportLatitude { get; private set; } = null;
        public decimal? AirportLongitude { get; private set; } = null;

        public static bool TryParse(string recordString, out Aff3 aff3)
        {
            return NASRDataParserGeneric<Aff3>.TryParse(recordString, 254, "AFF3", fieldList, out aff3);
        }

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 4, BuiltinTypeParsers.ParseString, nameof(ArtccIdent), false),
            (8, 30, BuiltinTypeParsers.ParseString, nameof(SiteLocation), false),
            (38, 5, FacilityTypeParser.ParseFacilityType, nameof(FacilityType), false),
            (43, 8, BuiltinTypeParsers.ParseDecimal, nameof(Frequency), false),
            (51, 10, ParseAltitudeSector, nameof(AltitudeSector), false),
            (61, 16, ParseFrequencyUsage, nameof(FrequencySpecialUsage), false),
            (77, 1, BuiltinTypeParsers.ParseBool, nameof(RCAGFrequencyCharted), true),
            (78, 4, BuiltinTypeParsers.ParseString, nameof(AirportIdent), true),
            (82, 30, BuiltinTypeParsers.ParseString, nameof(AirportState), true),
            (112, 2, BuiltinTypeParsers.ParseString, nameof(AirportStatePOCode), true),
            (114, 40, BuiltinTypeParsers.ParseString, nameof(AirportCity), true),
            (154, 50, BuiltinTypeParsers.ParseString, nameof(AirportName), true),
            (204, 14, ParseLatitudeLongitude.ParseLatLong, nameof(AirportLatitude), true),
            (229, 14, ParseLatitudeLongitude.ParseLatLong, nameof(AirportLongitude), true),
        };

        private static bool ParseAltitudeSector(string input, out object output)
        {
            output = null;
            switch (input)
            {
                case "LOW": output = AltitudeSector.Low; break;
                case "HIGH": output = AltitudeSector.High; break;
                case "LOW/HIGH": output = AltitudeSector.LowHigh; break;
                case "ULTRA-HIGH": output = AltitudeSector.UltraHigh; break;
                default: return false;
            }
            return true;
        }

        private static bool ParseFrequencyUsage(string input, out object output)
        {
            output = null;
            switch (input)
            {
                case "": output = FrequencySpecialUsage.None; break;
                case "APPROACH CONTROL": output = FrequencySpecialUsage.ApproachControl; break;
                case "DISCRETE": output = FrequencySpecialUsage.Discrete; break;
                case "DO NOT PUBLISH": output = FrequencySpecialUsage.DoNotPublish; break;
                case "OCEANIC": output = FrequencySpecialUsage.Oceanic; break;
                default: return false;
            }
            return true;
        }

        public bool Validate()
        {
            if (FacilityType == FacilityType.RemoteCommunicationsAirGround)
            {
                if (RCAGFrequencyCharted is null)
                {
                    return false;
                }
            }
            else
            {
                if (!(RCAGFrequencyCharted is null))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
