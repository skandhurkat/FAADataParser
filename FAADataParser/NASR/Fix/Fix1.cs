using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Fix
{
    public enum Type
    {
        Civilian,
        Military
    };
    public enum Use
    {
        ComputerNavigationFix,
        MilitaryReportingPoint,
        MilitaryWaypoint,
        NRSWaypoint,
        Radar,
        ReportingPoint,
        VFRWaypoint,
        Waypoint,
        RNAVWaypoint,
    };
    internal class Fix1 : INASRDataParser
    {
        public string ID { get; private set; }
        public string State { get; private set; }
        public string ICAORegionCode { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public Type Type { get; private set; }
        public bool Published { get; private set; }
        public Use Use { get; private set; }
        public string NASRIdentifier { get; private set; }
        public string HighARTCC { get; private set; }
        public string LowARTCC { get; private set; }
        public static bool TryParse(string recordString, out Fix1 fix1) => NASRDataParserGeneric<Fix1>.TryParse(recordString, 466, "FIX1", fieldList, out fix1);
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 30, BuiltinTypeParsers.ParseString, nameof(ID), false),
            (34, 30, BuiltinTypeParsers.ParseString, nameof(State), false),
            (64, 2, BuiltinTypeParsers.ParseString, nameof(ICAORegionCode), false),
            (66, 14, ParseLatitudeLongitude.ParseLatLong, nameof(Latitude), false),
            (80, 14, ParseLatitudeLongitude.ParseLatLong, nameof(Longitude), false),
            (94, 3, ParseType, nameof(Type), false),
            (212, 1, BuiltinTypeParsers.ParseBool, nameof(Published), false),
            (213, 15, ParseUse, nameof(Use), false),
            (228, 5, BuiltinTypeParsers.ParseString, nameof(NASRIdentifier), false),
            (233, 4, BuiltinTypeParsers.ParseString, nameof(HighARTCC), false),
            (237, 4, BuiltinTypeParsers.ParseString, nameof(LowARTCC), false),
        };
        private static bool ParseType(string input, out object type)
        {
            switch (input)
            {
                case "MIL": type = Type.Military; return true;
                case "FIX": type = Type.Civilian; return true;
                default: type = null; return false;
            }
        }
        private static bool ParseUse(string input, out object use)
        {
            switch (input)
            {
                case "CNF": use = Use.ComputerNavigationFix; return true;
                case "MIL-REP-PT": use = Use.MilitaryReportingPoint; return true;
                case "MIL-WAYPOINT": use = Use.MilitaryWaypoint; return true;
                case "NRS-WAYPOINT": use = Use.NRSWaypoint; return true;
                case "RADAR": use = Use.Radar; return true;
                case "REP-PT": use = Use.ReportingPoint; return true;
                case "VFR-WP": use = Use.VFRWaypoint; return true;
                case "WAYPOINT": use = Use.Waypoint; return true;
                case "RNAV-WP": use = Use.RNAVWaypoint; return true;
                default: use = null; return false;
            }
        }
        public bool Validate() => true;
    }
}
