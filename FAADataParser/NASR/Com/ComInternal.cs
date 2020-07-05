using System.Collections.Generic;
using System.Text.RegularExpressions;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Com
{
    public enum NavAid
    {
        NDB,
        VORTAC,
        VOR,
        VORDME,
        CONSOLAN,
        FanMarker,
        LowFrequencyRange,
        MarineNDB,
        VOT,
        DME,
        UHFNDB,
        NDBDME,
        TACAN
    };
    internal class ComInternal : INASRDataParser
    {
        public string Ident { get; private set; }
        public bool ColocatedWithNavaid { get; private set; }
        public string NavAidIdent { get; private set; }
        public NavAid? NavAidType { get; private set; }
        public string NavAidName { get; private set; }
        public decimal? NavAidLatitude { get; private set; }
        public decimal? NavAidLongitude { get; private set; }
        public decimal? OutletLatitude { get; private set; }
        public decimal? OutletLongitude { get; private set; }
        public string OutletCall { get; private set; }
        public List<(decimal Freq, bool ReceiveOnly)> Frequencies { get; private set; }
        public string FssIdent { get; private set; }
        public (string Ident, string Name) FssInfo { get; private set; }
        public string AlternateFssIdent { get; private set; }
        public (string Ident, string Name)? AlternateFssInfo { get; private set; }
        // TODO, important: Status field is not yet parsed
        public static bool TryParse(string recordString, out ComInternal com) => NASRDataParserGeneric<ComInternal>.TryParse(recordString, 693, "", fieldList, out com);
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (0, 4, BuiltinTypeParsers.ParseString, nameof(Ident), false),
            (11, 4, BuiltinTypeParsers.ParseString, nameof(NavAidIdent), true),
            (15, 2, ParseNavAidType, nameof(NavAidType), true),
            (63, 26, BuiltinTypeParsers.ParseString, nameof(NavAidName), true),
            (89, 14, ParseLatitudeLongitude.ParseLatLong, nameof(NavAidLatitude), true),
            (103, 14, ParseLatitudeLongitude.ParseLatLong, nameof(NavAidLongitude), true),
            (186, 14, ParseLatitudeLongitude.ParseLatLong, nameof(OutletLatitude), true),
            (200, 14, ParseLatitudeLongitude.ParseLatLong, nameof(OutletLongitude), true),
            (214, 26, BuiltinTypeParsers.ParseString, nameof(OutletCall), true),
            (240, 144, ParseFrequencies, nameof(Frequencies), false),
            (384, 04, BuiltinTypeParsers.ParseString, nameof(FssIdent), false),
            (388, 30, ParseFssIdentName, nameof(FssInfo), false),
            (418, 4, BuiltinTypeParsers.ParseString, nameof(AlternateFssIdent), true),
            (422, 30, ParseFssIdentName, nameof(AlternateFssInfo), true),
        };
        private static bool ParseNavAidType(string recordString, out object output)
        {
            switch (recordString)
            {
                case "R": output = NavAid.NDB; return true;
                case "C": output = NavAid.VORTAC; return true;
                case "V": output = NavAid.VOR; return true;
                case "D": output = NavAid.VORDME; return true;
                case "K": output = NavAid.CONSOLAN; return true;
                case "F": output = NavAid.FanMarker; return true;
                case "L": output = NavAid.LowFrequencyRange; return true;
                case "M": output = NavAid.MarineNDB; return true;
                case "O": output = NavAid.VOT; return true;
                case "OD": output = NavAid.DME; return true;
                case "U": output = NavAid.UHFNDB; return true;
                case "RD": output = NavAid.NDBDME; return true;
                case "T": output = NavAid.TACAN; return true;
                default: output = null; return false;
            }
        }
        private static bool ParseFrequencies(string recordString, out object listOfFrequencies)
        {
            const int fieldLen = 9;
            var receiveOnlyFreqRegex = new Regex(@"(?<Frequency>\d{3}\.\d{1,3})R");
            var freqList = new List<(decimal, bool)>();
            listOfFrequencies = null;
            while (recordString.Length > 0)
            {
                string token = recordString.Length > fieldLen ? recordString.Substring(0, fieldLen).Trim() : recordString;
                bool receiveOnly = false;
                recordString = recordString.Length > fieldLen ? recordString.Substring(9) : string.Empty;
                if (receiveOnlyFreqRegex.IsMatch(token))
                {
                    token = receiveOnlyFreqRegex.Match(token).Groups["Frequency"].Value;
                    receiveOnly = true;
                }
                if (!decimal.TryParse(token, out decimal freq))
                {
                    return false;
                }
                freqList.Add((freq, receiveOnly));
            }
            listOfFrequencies = freqList;
            return true;
        }
        private static bool ParseFssIdentName(string recordString, out object tupleIdentName)
        {
            string[] tokens = recordString.Split('*');
            tupleIdentName = null;
            if (tokens.Length != 2)
            {
                return false;
            }
            tupleIdentName = (tokens[0], tokens[1]);
            return true;
        }
        public bool Validate()
        {
            if (!(NavAidIdent is null))
            {
                ColocatedWithNavaid = true;
                if ((NavAidLatitude is null) || (NavAidLongitude is null) || (NavAidName is null) || (NavAidType is null)
                    || !(OutletLatitude is null) || !(OutletLongitude is null) || !(OutletCall is null))
                {
                    return false;
                }
            }
            else
            {
                ColocatedWithNavaid = false;
                if (!(NavAidLatitude is null) || !(NavAidLongitude is null) || !(NavAidName is null) || !(NavAidType is null)
                    || (OutletLatitude is null) || (OutletLongitude is null) || (OutletCall is null))
                {
                    return false;
                }
            }
            if (FssIdent != FssInfo.Ident)
            {
                return false;
            }
            if ((AlternateFssIdent is null) ^ (AlternateFssInfo is null))
            {
                return false;
            }
            if (!(AlternateFssIdent is null))
            {
                if (AlternateFssIdent != (((string Ident, string Name))AlternateFssInfo).Ident)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
