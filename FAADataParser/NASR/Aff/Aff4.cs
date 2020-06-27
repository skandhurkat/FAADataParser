using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Aff
{
    internal class Aff4 : INASRDataParser
    {
        public string ArtccIdent { get; private set; }
        public string SiteLocation { get; private set; }
        public FacilityType FacilityType { get; private set; }
        public decimal Frequency { get; private set; }
        public int RemarksNumber { get; private set; }
        public string RemarksText { get; private set; }

        public static bool TryParse(string recordString, out Aff4 aff4) => NASRDataParserGeneric<Aff4>.TryParse(recordString, 254, "AFF4", fieldList, out aff4);

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 4, BuiltinTypeParsers.ParseString, nameof(ArtccIdent), false),
            (8, 30, BuiltinTypeParsers.ParseString, nameof(SiteLocation), false),
            (38, 5, FacilityTypeParser.ParseFacilityType, nameof(FacilityType), false),
            (43, 8, BuiltinTypeParsers.ParseDecimal, nameof(Frequency), false),
            (51, 2, BuiltinTypeParsers.ParseInt, nameof(RemarksNumber), false),
            (53, 200, BuiltinTypeParsers.ParseString, nameof(RemarksText), false)
        };

        public bool Validate() => true;
    }
}
