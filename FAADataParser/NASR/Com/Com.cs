using System.Collections.Generic;

namespace FAADataParser.NASR.Com
{
    public class Com
    {
        public string Ident { get; set; }
        public NavAid? NavAidType { get; set; }
        public string NavAidIdent { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Call { get; set; }
        public List<(decimal Freq, bool ReceiveOnly)> Frequencies { get; set; }
        public string FssIdent { get; set; }
        public string FssName { get; set; }
        public string AlternateFssIdent { get; set; }
        public string AlternateFssName { get; set; }
        // TODO, important: Status field is not yet parsed
    }
}
