using System;
using System.Collections.Generic;
using System.IO;

namespace FAADataParser.NASR.Com
{
    public static class ComFileParser
    {
        public static List<Com> ParseFile(FileStream file)
        {
            var comList = new List<Com>();
            using (var streamReader = new StreamReader(file, true))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!ComInternal.TryParse(line, out ComInternal comInternal))
                    {
                        throw new ApplicationException("Could not parse\n" + line);
                    }
                    var com = new Com
                    {
                        Ident = comInternal.Ident,
                        Frequencies = comInternal.Frequencies,
                        FssIdent = comInternal.FssIdent,
                        FssName = comInternal.FssInfo.Name,
                        AlternateFssIdent = comInternal.AlternateFssIdent,
                        AlternateFssName = !(comInternal.AlternateFssInfo is null) ? (((string Ident, string Name))comInternal.AlternateFssInfo).Name : null
                    };
                    if (comInternal.ColocatedWithNavaid)
                    {
                        com.NavAidType = comInternal.NavAidType;
                        com.NavAidIdent = comInternal.NavAidIdent;
                        com.Latitude = (decimal)comInternal.NavAidLatitude;
                        com.Longitude = (decimal)comInternal.NavAidLongitude;
                        com.Call = comInternal.NavAidName;
                    }
                    else
                    {
                        com.NavAidType = null;
                        com.NavAidIdent = null;
                        com.Latitude = (decimal)comInternal.OutletLatitude;
                        com.Longitude = (decimal)comInternal.OutletLongitude;
                        com.Call = comInternal.OutletCall;
                    }
                    comList.Add(com);
                }
            }
            return comList;
        }
    }
}
