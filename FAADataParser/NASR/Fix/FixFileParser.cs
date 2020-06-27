using System;
using System.Collections.Generic;
using System.IO;

namespace FAADataParser.NASR.Fix
{
    public static class FixFileParser
    {
        public static List<Fix> ParseFile(FileStream file)
        {
            var fixes = new List<Fix>();
            using (var streamReader = new StreamReader(file, true))
            {
                string line = null;
                Fix fix = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    switch (line.Substring(0, 4))
                    {
                        case "FIX1":
                            {
                                if (!Fix1.TryParse(line, out Fix1 fix1))
                                {
                                    throw new ApplicationException("Could not parse FIX1\n" + line);
                                }
                                if (!(fix is null))
                                {
                                    fixes.Add(fix);
                                }
                                fix = new Fix
                                {
                                    ID = fix1.ID,
                                    State = fix1.State,
                                    ICAORegionCode = fix1.ICAORegionCode,
                                    Latitude = fix1.Latitude,
                                    Longitude = fix1.Longitude,
                                    Type = fix1.Type,
                                    Published = fix1.Published,
                                    Use = fix1.Use,
                                    NASRIdentifier = fix1.NASRIdentifier,
                                    HighARTCC = fix1.HighARTCC,
                                    LowARTCC = fix1.LowARTCC,
                                };
                                break;
                            }
                        case "FIX2":
                            {
                                if (!Fix2.TryParse(line, out Fix2 fix2))
                                {
                                    throw new ApplicationException("Could not parse FIX2\n" + line);
                                }
                                if (fix2.ID != fix.ID || fix2.State != fix.State || fix2.ICAORegionCode != fix.ICAORegionCode)
                                {
                                    throw new ApplicationException("Mismatch in FIX1 and FIX2 record\n" + line);
                                }
                                break;
                            }
                        case "FIX3":
                            {
                                if (!Fix3.TryParse(line, out Fix3 fix3))
                                {
                                    throw new ApplicationException("Could not parse FIX3\n" + line);
                                }
                                if (fix3.ID != fix.ID || fix3.State != fix.State || fix3.ICAORegionCode != fix.ICAORegionCode)
                                {
                                    throw new ApplicationException("Mismatch in FIX1 and FIX3 record\n" + line);
                                }
                                break;
                            }
                        case "FIX4":
                            {
                                if (!Fix4.TryParse(line, out Fix4 fix4))
                                {
                                    throw new ApplicationException("Could not parse FIX4\n" + line);
                                }
                                if (fix4.ID != fix.ID || fix4.State != fix.State || fix4.ICAORegionCode != fix.ICAORegionCode)
                                {
                                    throw new ApplicationException("Mismatch in FIX1 and FIX4 record\n" + line);
                                }
                                fix.Remarks.Add((fix4.Field, fix4.RemarkText));
                                break;
                            }
                        case "FIX5":
                            {
                                if (!Fix5.TryParse(line, out Fix5 fix5))
                                {
                                    throw new ApplicationException("Could not parse FIX5\n" + line);
                                }
                                if (fix5.ID != fix.ID || fix5.State != fix.State || fix5.ICAORegionCode != fix.ICAORegionCode)
                                {
                                    throw new ApplicationException("Mismatch in FIX1 and FIX5 record\n" + line);
                                }
                                break;
                            }
                        default:
                            {
                                throw new ApplicationException("Funny line\n" + line); // continue;
                            }
                    }
                }
                return fixes;
            }
        }
    }
}
