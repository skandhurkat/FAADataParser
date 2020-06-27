using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Fix
{
    internal class Fix4 : INASRDataParser
    {
        public string ID { get; set; }
        public string State { get; set; }
        public string ICAORegionCode { get; set; }
        public string Field { get; set; }
        public string RemarkText { get; set; }
        public static bool TryParse(string recordString, out Fix4 fix4) => NASRDataParserGeneric<Fix4>.TryParse(recordString, 466, "FIX4", fieldList, out fix4);

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 30, BuiltinTypeParsers.ParseString, nameof(ID), false),
            (34, 30, BuiltinTypeParsers.ParseString, nameof(State), false),
            (64, 2, BuiltinTypeParsers.ParseString, nameof(ICAORegionCode), false),
            (66, 100, BuiltinTypeParsers.ParseString, nameof(Field), false),
            (166, 300, BuiltinTypeParsers.ParseString, nameof(RemarkText), false),
        };

        public bool Validate() => true;
    }
}
