using System;
using System.Collections.Generic;
using FAADataParser.Utils;

namespace FAADataParser.NASR.Aff
{
    public enum FacilityType
    {
        AirRouteSurveillanceRadar,
        AirRouteTrafficControlCentre,
        CentreRadarApproachControlFacility,
        RemoteCommunicationsAirGround,
        SecondaryRadar
    };
    class Aff1 : INASRDataParser
    {
        public string ArtccIdent { get; private set; }
        public string ArtccName { get; private set; }
        public string SiteLocation { get; private set; }
        public string AltName { get; private set; }
        public FacilityType FacilityType { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public string StateName { get; private set; }
        public string StateCode { get; private set; }
        public decimal? Latitude { get; private set; } = null;
        public decimal? Longitude { get; private set; } = null;
        public string IcaoId { get; private set; }

        public static bool TryParse(string recordString, out Aff1 aff1)
        {
            return NASRDataParserGeneric<Aff1>.TryParse(recordString, 254, "AFF1", fieldList, out aff1);
        }

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 4, BuiltinTypeParsers.ParseString, nameof(ArtccIdent), false),
            (8, 40, BuiltinTypeParsers.ParseString, nameof(ArtccName), false),
            (48, 30, BuiltinTypeParsers.ParseString, nameof(SiteLocation), false),
            (78, 50, BuiltinTypeParsers.ParseString, nameof(AltName), true),
            (128, 5, ParseFacilityType, nameof(FacilityType), true),
            (133, 10, BuiltinTypeParsers.ParseDate, nameof(EffectiveDate), false),
            (143, 30, BuiltinTypeParsers.ParseString, nameof(StateName), false),
            (173, 2, BuiltinTypeParsers.ParseString, nameof(StateCode), false),
            (175, 14, ParseLatitudeLongitude.ParseLatLong, nameof(Latitude), true),
            (200, 14, ParseLatitudeLongitude.ParseLatLong, nameof(Longitude), true),
            (225, 4, BuiltinTypeParsers.ParseString, nameof(IcaoId), false)
        };

        private static bool ParseFacilityType(string value, out object facilityType)
        {
            facilityType = null;
            switch (value)
            {
                case "ARSR": facilityType = FacilityType.AirRouteSurveillanceRadar; break;
                case "ARTCC": facilityType = FacilityType.AirRouteTrafficControlCentre; break;
                case "CERAP": facilityType = FacilityType.CentreRadarApproachControlFacility; break;
                case "RCAG": facilityType = FacilityType.RemoteCommunicationsAirGround; break;
                case "SECRA": facilityType = FacilityType.SecondaryRadar; break;
                default: return false;
            }
            return true;
        }

        public bool Validate()
        {
            if (FacilityType == FacilityType.AirRouteSurveillanceRadar || FacilityType == FacilityType.SecondaryRadar)
            {
                if (!(Latitude is null) || !(Longitude is null))
                {
                    return false;
                }
            }
            else
            {
                if (Latitude is null || Longitude is null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
