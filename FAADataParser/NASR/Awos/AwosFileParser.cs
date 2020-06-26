using System;
using System.Collections.Generic;
using System.IO;

namespace FAADataParser.NASR.Awos
{
    public static class AwosFileParser
    {
        public static List<Awos> ParseFile(FileStream file)
        {
            var awosList = new List<Awos>();
            using (StreamReader streamReader = new StreamReader(file, true))
            {
                string line;
                Awos awos = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    switch (line.Substring(0, 5))
                    {
                        case "AWOS1":
                            {
                                if (!Awos1.TryParse(line, out Awos1 awos1))
                                {
                                    throw new ApplicationException("Could not parse AWOS1\n" + line);
                                }
                                if (!(awos is null))
                                {
                                    awosList.Add(awos);
                                }
                                awos = new Awos
                                {
                                    Identifier = awos1.SensorIdent,
                                    SensorType = awos1.SensorType,
                                    CommissioningStatus = awos1.CommissioningStatus,
                                    AssociatedWithNavaid = awos1.AssociatedWithNavaid,
                                    Latitude = awos1.Latitude,
                                    Longitude = awos1.Longitude,
                                    Elevation = awos1.Elevation,
                                    StationFrequency = awos1.StationFrequency,
                                    SecondaryStationFrequency = awos1.SecondaryStationFrequency,
                                    StationPhoneNumber = awos1.StationPhoneNumber,
                                    SecondaryStationPhoneNumber = awos1.SecondaryStationPhoneNumber,
                                    LandingFacilityComputerIdentifier = awos1.LandingFacilityComputerIdentifier,
                                    StationCity = awos1.StationCity,
                                    StationStatePOCode = awos1.StationStatePOCode,
                                    InformationEffectiveDate = awos1.InformationEffectiveDate
                                };
                                break;
                            }
                        case "AWOS2":
                            {
                                if (!Awos2.TryParse(line, out Awos2 awos2))
                                {
                                    throw new ApplicationException("Could not parse AWOS2\n" + line);
                                }
                                if (awos2.SensorIdent != awos.Identifier || awos2.SensorType != awos.SensorType)
                                {
                                    throw new ApplicationException("Mismatch in AWOS1 and AWOS2 records\n" + line);
                                }
                                awos.Remarks.Add(awos2.RemarkText);
                                break;
                            }
                        default:
                            {
                                throw new ApplicationException("Funny line\n" + line); // continue;
                            }
                    }
                }
            }
            return awosList;
        }
    }
}
