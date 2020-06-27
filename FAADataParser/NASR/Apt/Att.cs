using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    internal class Att : INASRDataParser
    {
        public string SiteNumber { get; private set; }
        public int SequenceNumber { get; private set; }
        public static bool TryParse(string recordString, out Att att) => NASRDataParserGeneric<Att>.TryParse(recordString, 1529, "ATT", fieldList, out att);
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (3, 11, BuiltinTypeParsers.ParseString, nameof(SiteNumber), false),
            (16, 2, BuiltinTypeParsers.ParseInt, nameof(SequenceNumber), false),
        };
        public bool Validate() => true;
    }
}
