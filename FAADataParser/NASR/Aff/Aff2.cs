using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Aff
{
    class Aff2 : INASRDataParser
    {
        public string ArtccIdent { get; private set; }
        public string SiteLocation { get; private set; }
        public FacilityType FacilityType { get; private set; }
        public int RemarksNumber { get; private set; }
        public string RemarksText { get; private set; }

        public static bool TryParse(string recordString, out Aff2 aff2)
        {
            return NASRDataParserGeneric<Aff2>.TryParse(recordString, 254, "AFF2", fieldList, out aff2);
        }

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (4, 4, BuiltinTypeParsers.ParseString, nameof(ArtccIdent), false),
            (8, 30, BuiltinTypeParsers.ParseString, nameof(SiteLocation), false),
            (38, 5, FacilityTypeParser.ParseFacilityType, nameof(FacilityType), false),
            (43, 4, BuiltinTypeParsers.ParseInt, nameof(RemarksNumber), false),
            (47, 200, BuiltinTypeParsers.ParseString, nameof(RemarksText), false)
        };

        public bool Validate() => true;
    }
}
