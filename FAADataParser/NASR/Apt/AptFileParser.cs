using System;
using System.Collections.Generic;
using System.IO;

namespace FAADataParser.NASR.Apt
{
    public static class AptFileParser
    {
        public static List<Airport> ParseFile(FileStream file)
        {
            var airports = new List<Airport>();
            using (var streamReader = new StreamReader(file, true))
            {
                string line;
                Airport airport = null;
                string siteNumber = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    switch (line.Substring(0, 3))
                    {
                        case "APT":
                            {
                                if (!Apt.TryParse(line, out Apt apt))
                                {
                                    throw new ApplicationException("Could not parse APT\n" + line);
                                }
                                if (!(airport is null))
                                {
                                    airports.Add(airport);
                                }
                                siteNumber = apt.SiteNumber;
                                airport = new Airport
                                {
                                    FAAComputerIdentifier = apt.SiteNumber,
                                    LandingFacilityType = apt.LandingFacilityType,
                                    LocationIdentifier = apt.LocationIdentifier,
                                    InformationEffectiveDate = apt.InformationEffectiveDate,
                                    FAAFieldOffice = apt.FAAFieldOffice,
                                    StatePostOfficeCode = apt.StatePostOfficeCode,
                                    StateName = apt.StateName,
                                    CountyName = apt.CountyName,
                                    CountyStatePostOfficeCode = apt.CountyStatePostOfficeCode,
                                    CityName = apt.CityName,
                                    FacilityName = apt.FacilityName,
                                    AirportOwnershipType = apt.AirportOwnershipType,
                                    FacilityUse = apt.FacilityUse,
                                    OwnersName = apt.OwnersName,
                                    OwnersAddress1 = apt.OwnersAddress1,
                                    OwnersAddress2 = apt.OwnersAddress2,
                                    OwnersPhone = apt.OwnersPhone,
                                    ManagersName = apt.ManagersName,
                                    ManagersAddress1 = apt.ManagersAddress1,
                                    ManagersAddress2 = apt.ManagersAddress2,
                                    ManagersPhone = apt.ManagersPhone,
                                    Latitude = apt.Latitude,
                                    Longitude = apt.Longitude,
                                    ReferencePointDeterminationMethod = apt.ReferencePointDeterminationMethod,
                                    Elevation = apt.Elevation,
                                    ElevationDeterminationMethod = apt.ElevationDeterminationMethod,
                                    MagneticVariation = apt.MagneticVariation,
                                    MagneticVariationEpochYear = apt.MagneticVariationEpochYear,
                                    TPA = apt.TPA,
                                    SectionalName = apt.SectionalName,
                                    DistanceFromCBDInNm = apt.DistanceFromCBDInNm,
                                    DirectionFromCBD = apt.DirectionFromCBD,
                                    BoundaryArtccIdentifier = apt.BoundaryArtccIdentifier,
                                    BoundaryArtccFaaComputerIdentifier = apt.BoundaryArtccFaaComputerIdentifier,
                                    BoundaryArtccName = apt.BoundaryArtccName,
                                    ResponsibleArtccIdentifier = apt.ResponsibleArtccIdentifier,
                                    ResponsibleArtccFaaComputerIdentifier = apt.ResponsibleArtccFaaComputerIdentifier,
                                    ResponsibleArtccName = apt.ResponsibleArtccName,
                                    TieInFssOnFacility = (bool)apt.TieInFssOnFacility,
                                    TieInFssIdentifier = apt.TieInFssIdentifier,
                                    TieInFssName = apt.TieInFssName,
                                    LocalFssPhoneNumber = apt.LocalFssPhoneNumber,
                                    TollFreeFssPhoneNumber = apt.TollFreeFssPhoneNumber,
                                    AlternateFssIdentifier = apt.AlternateFssIdentifier,
                                    AlternateFssName = apt.AlternateFssName,
                                    TollFreeAlternateFssNumber = apt.TollFreeAlternateFssNumber,
                                    NotamAndWeatherFacilityIdentifier = apt.NotamAndWeatherFacilityIdentifier,
                                    NotamDAvailableAtAirport = (bool)apt.NotamDAvailableAtAirport
                                };
                                break;
                            }
                        case "ATT":
                            {
                                if (!Att.TryParse(line, out Att att))
                                {
                                    throw new ApplicationException("Could not parse ATT\n" + line);
                                }
                                if (att.SiteNumber != siteNumber)
                                {
                                    throw new ApplicationException("Mismatch in APT and ATT\n" + line);
                                }
                                break;
                            }
                        case "RWY":
                            {
                                if (!Rwy.TryParse(line, out Rwy rwy))
                                {
                                    if (Rwy.instrumentRunwayBogusEntry.IsMatch(rwy.RwyIdentification))
                                    {
                                        // These are entries that seem to be used for instrument approaches to runways.
                                        // Don't know how to handle these, so won't be handling for now.
                                        continue;
                                    }
                                    else
                                    {
                                        throw new ApplicationException("Could not parse RWY\n" + line);
                                    }
                                }
                                if (rwy.SiteNumber != siteNumber)
                                {
                                    throw new ApplicationException("Mismatch in APT and RWY\n" + line);
                                }
                                foreach (ILandingStructure landingStructure in airport.LandingStructures)
                                {
                                    if (landingStructure.Identification == rwy.RwyIdentification)
                                    {
                                        throw new ApplicationException("Duplicate RWY entry\n" + line);
                                    }
                                }
                                if (Rwy.helipadRegex.IsMatch(rwy.RwyIdentification))
                                {
                                    airport.LandingStructures.Add(item: new Helipad
                                    {
                                        Identification = rwy.RwyIdentification,
                                        Latitude = rwy.BaseEndLatitude,
                                        Longitude = rwy.BaseEndLongitude,
                                        Elevation = rwy.BaseEndElevation,
                                        SurfaceType = rwy.RunwaySurface.surfaceTypes,
                                        SurfaceCondition = rwy.RunwaySurface.surfaceCondition
                                    });
                                }
                                else if (Rwy.balloonportRegex.IsMatch(rwy.RwyIdentification))
                                {
                                    airport.LandingStructures.Add(item: new BalloonPort
                                    {
                                        Identification = rwy.RwyIdentification,
                                        LengthInFeet = rwy.LengthInFeet,
                                        WidthInFeet = rwy.WidthInFeet,
                                        Latitude = rwy.BaseEndLatitude,
                                        Longitude = rwy.BaseEndLongitude,
                                        Elevation = rwy.BaseEndElevation,
                                        SurfaceType = rwy.RunwaySurface.surfaceTypes,
                                        SurfaceCondition = rwy.RunwaySurface.surfaceCondition
                                    });
                                }
                                else if (Rwy.runwayRegex.IsMatch(rwy.RwyIdentification))
                                {
                                    airport.LandingStructures.Add(item: new Runway
                                    {
                                        Identification = rwy.RwyIdentification,
                                        LengthInFeet = rwy.LengthInFeet,
                                        WidthInFeet = rwy.WidthInFeet,
                                        SurfaceType = rwy.RunwaySurface.surfaceTypes,
                                        SurfaceCondition = rwy.RunwaySurface.surfaceCondition,
                                        BaseEndIdentifier = rwy.BaseEndIdentifier,
                                        BaseEndTrueAlignment = rwy.BaseEndTrueAlignment,
                                        BaseEndRightTraffic = (bool)rwy.BaseEndRightTraffic,
                                        BaseEndLatitude = rwy.BaseEndLatitude,
                                        BaseEndLongitude = rwy.BaseEndLongitude,
                                        BaseEndElevation = rwy.BaseEndElevation,
                                        BaseEndDisplacedThresholdLatitude = rwy.BaseEndDisplacedThresholdLatitude,
                                        BaseEndDisplacedThresholdLongitude = rwy.BaseEndDisplacedThresholdLongitude,
                                        BaseEndDisplacedThresholdElevation = rwy.BaseEndDisplacedThresholdElevation,
                                        BaseEndTouchdownZoneElevation = rwy.BaseEndTouchdownZoneElevation,
                                        ReciprocalEndIdentifier = rwy.ReciprocalEndIdentifier,
                                        ReciprocalEndTrueAlignment = rwy.ReciprocalEndTrueAlignment,
                                        ReciprocalEndRightTraffic = (bool)rwy.ReciprocalEndRightTraffic,
                                        ReciprocalEndLatitude = rwy.ReciprocalEndLatitude,
                                        ReciprocalEndLongitude = rwy.ReciprocalEndLongitude,
                                        ReciprocalEndElevation = rwy.ReciprocalEndElevation,
                                        ReciprocalEndDisplacedThresholdLatitude = rwy.ReciprocalEndDisplacedThresholdLatitude,
                                        ReciprocalEndDisplacedThresholdLongitude = rwy.ReciprocalEndDisplacedThresholdLongitude,
                                        ReciprocalEndDisplacedThresholdElevation = rwy.ReciprocalEndDisplacedThresholdElevation,
                                        ReciprocalEndTouchdownZoneElevation = rwy.ReciprocalEndTouchdownZoneElevation
                                    });
                                }
                                else
                                {
                                    throw new ApplicationException("Something went wrong with RWY regexes\n" + line);
                                }
                                break;
                            }
                        case "ARS":
                            {
                                if (!Ars.TryParse(line, out Ars ars))
                                {
                                    throw new ApplicationException("Could not parse ARS\n" + line);
                                }
                                if (ars.SiteNumber != siteNumber)
                                {
                                    throw new ApplicationException("Mismatch in APT and ARS\n" + line);
                                }
                                bool found = false;
                                foreach (Runway rwy in airport.LandingStructures)
                                {
                                    if (rwy.Identification == ars.RwyIdentification)
                                    {
                                        if (rwy.BaseEndIdentifier == ars.RwyEndIdentifier || rwy.ReciprocalEndIdentifier == ars.RwyEndIdentifier)
                                        {
                                            found = true;
                                        }
                                        else
                                        {
                                            throw new ApplicationException("Could not locate runway end identifier\n" + line);
                                        }
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    throw new ApplicationException("Could not find runway for ARS\n" + line);
                                }
                                break;
                            }
                        case "RMK":
                            {
                                if (!Rmk.TryParse(line, out Rmk rmk))
                                {
                                    throw new ApplicationException("Could not parse RMK\n" + line);
                                }
                                if (rmk.SiteNumber != siteNumber)
                                {
                                    throw new ApplicationException("Mismatch in APT and RMK\n" + line);
                                }
                                break;
                            }
                        default: throw new ApplicationException("Funny line\n" + line); // continue;
                    }
                }
            }
            return airports;
        }
    }
}
