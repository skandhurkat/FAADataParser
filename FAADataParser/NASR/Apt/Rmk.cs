using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    internal class Rmk : INASRDataParser
    {
        public string SiteNumber { get; private set; }
        public string ElementName { get; private set; }
        public string Text { get; private set; }
        public static bool TryParse(string recordString, out Rmk rmk) => NASRDataParserGeneric<Rmk>.TryParse(recordString, 1529, "RMK", fieldList, out rmk);
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (3, 11, BuiltinTypeParsers.ParseString, nameof(SiteNumber), false),
            (16, 13, BuiltinTypeParsers.ParseString, nameof(ElementName), false),
            (29, 1500, BuiltinTypeParsers.ParseString, nameof(Text), false),
        };
        public bool Validate() => true;
    }
}
