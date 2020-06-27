using System.Collections.Generic;

namespace FAADataParser.NASR.Fix
{
    public class Fix
    {
        public string ID { get; set; }
        public string State { get; set; }
        public string ICAORegionCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Type Type { get; set; }
        public bool Published { get; set; }
        public Use Use { get; set; }
        public string NASRIdentifier { get; set; }
        public string HighARTCC { get; set; }
        public string LowARTCC { get; set; }
        public List<(string Field, string Text)> Remarks { get; set; } = new List<(string Field, string Text)>();
    }
}
