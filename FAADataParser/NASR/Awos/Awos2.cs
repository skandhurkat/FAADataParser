using System.Collections.Generic;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Awos
{
    class Awos2 : INASRDataParser
    {
        public string SensorIdent { get; private set; }
        public WxSensorType SensorType { get; private set; }
        public string RemarkText { get; private set; }
        public static bool TryParse(string recordString, out Awos2 awos2) => NASRDataParserGeneric<Awos2>.TryParse(recordString, 255, "AWOS2", fieldList, out awos2);
        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (5, 4, BuiltinTypeParsers.ParseString, nameof(SensorIdent), false),
            (9, 10, Awos1.ParseWxSensorType, nameof(SensorType), false),
            (19, 236, BuiltinTypeParsers.ParseString, nameof(RemarkText), false)
        };
        public bool Validate() => true;
    }
}
