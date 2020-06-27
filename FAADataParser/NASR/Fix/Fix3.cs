using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Fix
{
    internal class Fix3 : INASRDataParser
    {
        public string ID { get; private set; }
        public string State { get; private set; }
        public string ICAORegionCode { get; private set; }
        public static bool TryParse(string recordString, out Fix3 fix3) => NASRDataParserGeneric<Fix3>.TryParse(recordString, 466, "FIX3", fieldList, out fix3);

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 30, BuiltinTypeParsers.ParseString, nameof(ID), false),
            (34, 30, BuiltinTypeParsers.ParseString, nameof(State), false),
            (64, 2, BuiltinTypeParsers.ParseString, nameof(ICAORegionCode), false),
        };

        public bool Validate() => true;
    }
}
