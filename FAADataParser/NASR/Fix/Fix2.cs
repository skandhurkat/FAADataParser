using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Fix
{
    internal class Fix2 : INASRDataParser
    {
        public string ID { get; private set; }
        public string State { get; private set; }
        public string ICAORegionCode { get; private set; }
        public static bool TryParse(string recordString, out Fix2 fix2) => NASRDataParserGeneric<Fix2>.TryParse(recordString, 466, "FIX2", fieldList, out fix2);

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 30, BuiltinTypeParsers.ParseString, nameof(ID), false),
            (34, 30, BuiltinTypeParsers.ParseString, nameof(State), false),
            (64, 2, BuiltinTypeParsers.ParseString, nameof(ICAORegionCode), false),
        };

        public bool Validate() => true;
    }
}
