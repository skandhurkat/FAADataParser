using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Awos
{
    public enum WxSensorType
    {
        ASOS,
        AWOS_1,
        AWOS_2,
        AWOS_3,
        AWOS_4,
        AWOS_A,
        ASOS_A,
        ASOS_B,
        ASOS_C,
        ASOS_D,
        AWSS,
        AWOS_3T,
        AWOS_3P,
        AWOS_3PT,
        AWOS_AV,
        WEF,
        SAWS
    };

    internal class Awos1 : INASRDataParser
    {
        public string SensorIdent { get; private set; }
        public WxSensorType SensorType { get; private set; }
        public bool CommissioningStatus { get; private set; }
        public bool AssociatedWithNavaid { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public decimal? Elevation { get; private set; }
        public decimal? StationFrequency { get; private set; }
        public decimal? SecondaryStationFrequency { get; private set; }
        [Phone]
        public string StationPhoneNumber { get; private set; }
        [Phone]
        public string SecondaryStationPhoneNumber { get; private set; }
        public string LandingFacilityComputerIdentifier { get; private set; }
        public string StationCity { get; private set; }
        public string StationStatePOCode { get; private set; }
        public DateTime InformationEffectiveDate { get; private set; }
        public static bool TryParse(string recordString, out Awos1 awos1) => NASRDataParserGeneric<Awos1>.TryParse(recordString, 255, "AWOS1", fieldList, out awos1);
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (5, 4, BuiltinTypeParsers.ParseString, nameof(SensorIdent), false),
            (9, 10, ParseWxSensorType, nameof(SensorType), false),
            (19, 1, BuiltinTypeParsers.ParseBool, nameof(CommissioningStatus), false),
            (30, 1, BuiltinTypeParsers.ParseBool, nameof(AssociatedWithNavaid), false),
            (31, 14, ParseLatitudeLongitude.ParseLatLong, nameof(Latitude), true),
            (45, 15, ParseLatitudeLongitude.ParseLatLong, nameof(Longitude), true),
            (60, 7, BuiltinTypeParsers.ParseDecimal, nameof(Elevation), true),
            (68, 7, BuiltinTypeParsers.ParseDecimal, nameof(StationFrequency), true),
            (75, 7, BuiltinTypeParsers.ParseDecimal, nameof(SecondaryStationFrequency), true),
            (82, 14, ParsePhone.TryParse, nameof(StationPhoneNumber), true),
            (96, 14, ParsePhone.TryParse, nameof(SecondaryStationPhoneNumber), true),
            (121, 40, BuiltinTypeParsers.ParseString, nameof(StationCity), false),
            (161, 2, BuiltinTypeParsers.ParseString, nameof(StationStatePOCode), false),
            (163, 10, BuiltinTypeParsers.ParseDate, nameof(InformationEffectiveDate), false)
        };
        internal static bool ParseWxSensorType(string recordString, out object wxSensorType)
        {
            switch (recordString)
            {
                case "ASOS": wxSensorType = WxSensorType.ASOS; return true;
                case "ASOS-A": wxSensorType = WxSensorType.ASOS_A; return true;
                case "ASOS-B": wxSensorType = WxSensorType.ASOS_B; return true;
                case "ASOS-C": wxSensorType = WxSensorType.ASOS_C; return true;
                case "ASOS-D": wxSensorType = WxSensorType.ASOS_D; return true;
                case "AWOS-1": wxSensorType = WxSensorType.AWOS_1; return true;
                case "AWOS-2": wxSensorType = WxSensorType.AWOS_2; return true;
                case "AWOS-3": wxSensorType = WxSensorType.AWOS_3; return true;
                case "AWOS-4": wxSensorType = WxSensorType.AWOS_4; return true;
                case "AWOS-A": wxSensorType = WxSensorType.AWOS_A; return true;
                case "AWOS-3T": wxSensorType = WxSensorType.AWOS_3T; return true;
                case "AWOS-3P": wxSensorType = WxSensorType.AWOS_3P; return true;
                case "AWOS-3PT": wxSensorType = WxSensorType.AWOS_3PT; return true;
                case "AWOS-AV": wxSensorType = WxSensorType.AWOS_AV; return true;
                case "AWSS": wxSensorType = WxSensorType.AWSS; return true;
                case "WEF": wxSensorType = WxSensorType.WEF; return true;
                case "SAWS": wxSensorType = WxSensorType.SAWS; return true;
                default: wxSensorType = null; return false;
            }
        }
        public bool Validate()
        {
            return (!(StationFrequency is null) || SecondaryStationFrequency is null)
                && (!(StationPhoneNumber is null) || SecondaryStationPhoneNumber is null)
                && !((Latitude is null) ^ (Longitude is null));
        }
    }
}
