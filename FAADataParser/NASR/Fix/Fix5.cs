using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Fix
{
    internal class Fix5 : INASRDataParser
    {
        public string ID { get; set; }
        public string State { get; set; }
        public string ICAORegionCode { get; set; }
        public static bool TryParse(string recordString, out Fix5 fix5) => NASRDataParserGeneric<Fix5>.TryParse(recordString, 466, "FIX5", fieldList, out fix5);

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 30, BuiltinTypeParsers.ParseString, nameof(ID), false),
            (34, 30, BuiltinTypeParsers.ParseString, nameof(State), false),
            (64, 2, BuiltinTypeParsers.ParseString, nameof(ICAORegionCode), false),
        };

        public bool Validate() => true;
    }
}
