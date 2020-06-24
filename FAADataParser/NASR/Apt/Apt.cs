using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    public enum LandingFacilityType
    {
        Airport,
        Balloonport,
        SeaplaneBase,
        Gliderport,
        Heliport,
        Ultralight
    };
    public enum AirportOwnershipType
    {
        PubliclyOwned,
        PrivatelyOwned,
        AirforceOwned,
        NavyOwned,
        ArmyOwned,
        CoastGuardOwned
    };
    public enum FacilityUse { Public, Private };
    public enum SurveyMethod { Surveyed, Estimated };

    class Apt : INASRDataParser
    {
        public string SiteNumber { get; private set; }
        public LandingFacilityType LandingFacilityType { get; private set; }
        public string LocationIdentifier { get; private set; }
        public DateTime InformationEffectiveDate { get; private set; }
        public string FAAFieldOffice { get; private set; }
        public string StatePostOfficeCode { get; private set; }
        public string StateName { get; private set; }
        public string CountyName { get; private set; }
        public string CountyStatePostOfficeCode { get; private set; }
        public string CityName { get; private set; }
        public string FacilityName { get; private set; }
        public AirportOwnershipType AirportOwnershipType { get; private set; }
        public FacilityUse FacilityUse { get; private set; }
        public string OwnersName { get; private set; }
        public string OwnersAddress1 { get; private set; }
        public string OwnersAddress2 { get; private set; }
        [Phone]
        public string OwnersPhone { get; private set; }
        public string ManagersName { get; private set; }
        public string ManagersAddress1 { get; private set; }
        public string ManagersAddress2 { get; private set; }
        [Phone]
        public string ManagersPhone { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public SurveyMethod ReferencePointDeterminationMethod { get; private set; }
        public decimal Elevation { get; private set; }
        public SurveyMethod? ElevationDeterminationMethod { get; private set; }
        public int? MagneticVariation { get; private set; }
        public int? MagneticVariationEpochYear { get; private set; }
        public int? TPA { get; private set; }
        public string SectionalName { get; private set; }
        public int? DistanceFromCBDInNm { get; private set; }
        public CompassPoint? DirectionFromCBD { get; private set; }
        public string BoundaryArtccIdentifier { get; private set; }
        public string BoundaryArtccFaaComputerIdentifier { get; private set; }
        public string BoundaryArtccName { get; private set; }
        public string ResponsibleArtccIdentifier { get; private set; }
        public string ResponsibleArtccFaaComputerIdentifier { get; private set; }
        public string ResponsibleArtccName { get; private set; }
        public bool? TieInFssOnFacility { get; private set; }
        public string TieInFssIdentifier { get; private set; }
        public string TieInFssName { get; private set; }
        [Phone]
        public string LocalFssPhoneNumber { get; private set; }
        [Phone]
        public string TollFreeFssPhoneNumber { get; private set; }
        public string AlternateFssIdentifier { get; private set; }
        public string AlternateFssName { get; private set; }
        [Phone]
        public string TollFreeAlternateFssNumber { get; private set; }
        public string NotamAndWeatherFacilityIdentifier { get; private set; }
        public bool? NotamDAvailableAtAirport { get; private set; }
        public static bool TryParse(string recordString, out Apt apt)
        {
            return NASRDataParserGeneric<Apt>.TryParse(recordString, 1529, "APT", fieldList, out apt);
        }

        private static readonly List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)> fieldList = new List<(int fieldStart, int fieldLength, ParserDelegate parserDelegate, string propertyName, bool nullable)>
        {
            (3, 11, BuiltinTypeParsers.ParseString, nameof(SiteNumber), false),
            (14, 13, LandingFacilityParser, nameof(LandingFacilityType), false),
            (27, 4, BuiltinTypeParsers.ParseString, nameof(LocationIdentifier), false),
            (31, 10,  BuiltinTypeParsers.ParseDate, nameof(InformationEffectiveDate), false),
            (44, 4, BuiltinTypeParsers.ParseString, nameof(FAAFieldOffice), false),
            (48, 2, BuiltinTypeParsers.ParseString, nameof(StatePostOfficeCode), false),
            (50, 20, BuiltinTypeParsers.ParseString, nameof(StateName), false),
            (70, 21, BuiltinTypeParsers.ParseString, nameof(CountyName), false),
            (91, 2, BuiltinTypeParsers.ParseString, nameof(CountyStatePostOfficeCode), false),
            (93, 40, BuiltinTypeParsers.ParseString, nameof(CityName), false),
            (133, 50, BuiltinTypeParsers.ParseString, nameof(FacilityName), false),
            (183, 2, AirportOwnershipTypeParser, nameof(AirportOwnershipType), false),
            (185, 2, FacilityUseParser, nameof(FacilityUse), false),
            (187, 35, BuiltinTypeParsers.ParseString, nameof(OwnersName), true),
            (222, 72, BuiltinTypeParsers.ParseString, nameof(OwnersAddress1), true),
            (294, 45, BuiltinTypeParsers.ParseString, nameof(OwnersAddress2), true),
            (339, 16, ParsePhone.TryParse, nameof(OwnersPhone), true),
            (355, 35, BuiltinTypeParsers.ParseString, nameof(ManagersName), true),
            (390, 72, BuiltinTypeParsers.ParseString, nameof(ManagersAddress1), true),
            (462, 45, BuiltinTypeParsers.ParseString, nameof(ManagersAddress2), true),
            (507, 16, ParsePhone.TryParse, nameof(ManagersPhone), true),
            (523, 15, ParseLatitudeLongitude.ParseLatLong, nameof(Latitude), false),
            (550, 15, ParseLatitudeLongitude.ParseLatLong, nameof(Longitude), false),
            (577, 1, SurveyMethodParser, nameof(ReferencePointDeterminationMethod), false),
            (578, 7, BuiltinTypeParsers.ParseDecimal, nameof(Elevation), false),
            (585, 1, SurveyMethodParser, nameof(ElevationDeterminationMethod), true),
            (586, 3, MagVarParser, nameof(MagneticVariation), true),
            (589, 4, BuiltinTypeParsers.ParseInt, nameof(MagneticVariationEpochYear), true),
            (593, 4, BuiltinTypeParsers.ParseInt, nameof(TPA), true),
            (597, 30, BuiltinTypeParsers.ParseString, nameof(SectionalName), false),
            (627, 2, BuiltinTypeParsers.ParseInt, nameof(DistanceFromCBDInNm), true),
            (629, 3, ParseCompassPoint.TryParse, nameof(DirectionFromCBD), true),
            (637, 4, BuiltinTypeParsers.ParseString, nameof(BoundaryArtccIdentifier), false),
            (641, 3, BuiltinTypeParsers.ParseString, nameof(BoundaryArtccFaaComputerIdentifier), false),
            (644, 30, BuiltinTypeParsers.ParseString, nameof(BoundaryArtccName), false),
            (674, 4, BuiltinTypeParsers.ParseString, nameof(ResponsibleArtccIdentifier), false),
            (678, 3, BuiltinTypeParsers.ParseString, nameof(ResponsibleArtccFaaComputerIdentifier), false),
            (681, 30, BuiltinTypeParsers.ParseString, nameof(ResponsibleArtccName), false),
            (711, 1, BuiltinTypeParsers.ParseBool, nameof(TieInFssOnFacility), true),
            (712, 4, BuiltinTypeParsers.ParseString, nameof(TieInFssIdentifier), true),
            (716, 30, BuiltinTypeParsers.ParseString, nameof(TieInFssName), true),
            (746, 16, ParsePhone.TryParse, nameof(LocalFssPhoneNumber), true),
            (762, 16, ParsePhone.TryParse, nameof(TollFreeFssPhoneNumber), true),
            (778, 4, BuiltinTypeParsers.ParseString, nameof(AlternateFssIdentifier), true),
            (782, 30, BuiltinTypeParsers.ParseString, nameof(AlternateFssName), true),
            (812, 16, ParsePhone.TryParse, nameof(TollFreeAlternateFssNumber), true),
            (828, 4, BuiltinTypeParsers.ParseString, nameof(NotamAndWeatherFacilityIdentifier), false),
            (832, 1, BuiltinTypeParsers.ParseBool, nameof(NotamDAvailableAtAirport), true)
        };
        public bool Validate()
        {
            if (TieInFssOnFacility is null)
            {
                TieInFssOnFacility = false;
            }
            if (TieInFssOnFacility == true)
            {
                if ((TieInFssName is null) || (TieInFssIdentifier is null))
                {
                    return false;
                }
            }
            if ((MagneticVariation is null) && !(MagneticVariationEpochYear is null))
            {
                // Should return false, but this is stupid.
                // return false;
            }
            if (NotamDAvailableAtAirport is null)
            {
                NotamDAvailableAtAirport = true;
            }
            if (DirectionFromCBD is null)
            {
                if (!(DistanceFromCBDInNm is null || DistanceFromCBDInNm == 0))
                {
                    // Well, some airports don't specify the direction from CBD even when distance is specified
                    // return false;
                }
            }
            return true;
        }

        private static bool LandingFacilityParser(string val, out object landingFacilityType)
        {
            switch (val)
            {
                case "AIRPORT": landingFacilityType = LandingFacilityType.Airport; break;
                case "BALLOONPORT": landingFacilityType = LandingFacilityType.Balloonport; break;
                case "SEAPLANE BASE": landingFacilityType = LandingFacilityType.SeaplaneBase; break;
                case "GLIDERPORT": landingFacilityType = LandingFacilityType.Gliderport; break;
                case "HELIPORT": landingFacilityType = LandingFacilityType.Heliport; break;
                case "ULTRALIGHT": landingFacilityType = LandingFacilityType.Ultralight; break;
                default: landingFacilityType = null; return false;
            }
            return true;
        }
        private static bool AirportOwnershipTypeParser(string val, out object airportOwnershipType)
        {
            switch (val)
            {
                case "PU": airportOwnershipType = AirportOwnershipType.PubliclyOwned; break;
                case "PR": airportOwnershipType = AirportOwnershipType.PrivatelyOwned; break;
                case "MA": airportOwnershipType = AirportOwnershipType.AirforceOwned; break;
                case "MN": airportOwnershipType = AirportOwnershipType.NavyOwned; break;
                case "MR": airportOwnershipType = AirportOwnershipType.ArmyOwned; break;
                case "CG": airportOwnershipType = AirportOwnershipType.CoastGuardOwned; break;
                default: airportOwnershipType = null; return false;
            }
            return true;
        }
        private static bool FacilityUseParser(string val, out object facilityUse)
        {
            switch (val)
            {
                case "PU": facilityUse = FacilityUse.Public; break;
                case "PR": facilityUse = FacilityUse.Private; break;
                default: facilityUse = null; return false;
            }
            return true;
        }
        private static bool SurveyMethodParser(string val, out object surveyMethod)
        {
            switch (val)
            {
                case "E": surveyMethod = SurveyMethod.Estimated; break;
                case "S": surveyMethod = SurveyMethod.Surveyed; break;
                default: surveyMethod = null; return false;
            }
            return true;
        }
        private static bool MagVarParser(string val, out object magVar)
        {
            magVar = null;
            Match match = magVarRegex.Match(val);
            if (match.Success)
            {
                _ = BuiltinTypeParsers.ParseInt(match.Groups["Amount"].Value, out magVar);
                if (match.Groups["Direction"].Value == "E")
                {
                    magVar = -(int)magVar;
                }
                return true;
            }
            return false;
        }

        private static readonly Regex magVarRegex = new Regex(@"\b(?<Amount>\d{2})(?<Direction>[EW])\b");
    }
}
