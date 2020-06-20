using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    class Ars : INASRDataParser
    {
        public string SiteNumber { get; private set; }
        public string RwyIdentification { get; private set; }
        public string RwyEndIdentifier { get; private set; }
        public static bool TryParse(string recordString, out Ars ars)
        {
            return NASRDataParserGeneric<Ars>.TryParse(recordString, 1529, "ARS", fieldList, out ars);
        }
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (3, 11, BuiltinTypeParsers.ParseString, nameof(SiteNumber), false),
            (16, 7, BuiltinTypeParsers.ParseString, nameof(RwyIdentification), false),
            (23, 3, BuiltinTypeParsers.ParseString, nameof(RwyEndIdentifier), false),
        };
        public bool Validate() => true;
    }
}
