using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using FAADataParser.Utils;

namespace FAADataParser.NASR.Apt
{
    public class Airport
    {
        public string FAAComputerIdentifier { get; set; }
        public LandingFacilityType LandingFacilityType { get; set; }
        public string LocationIdentifier { get; set; }
        public DateTime InformationEffectiveDate { get; set; }
        public string FAAFieldOffice { get; set; }
        public string StatePostOfficeCode { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CountyStatePostOfficeCode { get; set; }
        public string CityName { get; set; }
        public string FacilityName { get; set; }
        public AirportOwnershipType AirportOwnershipType { get; set; }
        public FacilityUse FacilityUse { get; set; }
        public string OwnersName { get; set; }
        public string OwnersAddress1 { get; set; }
        public string OwnersAddress2 { get; set; }
        [Phone]
        public string OwnersPhone { get; set; }
        public string ManagersName { get; set; }
        public string ManagersAddress1 { get; set; }
        public string ManagersAddress2 { get; set; }
        [Phone]
        public string ManagersPhone { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public SurveyMethod ReferencePointDeterminationMethod { get; set; }
        public decimal Elevation { get; set; }
        public SurveyMethod? ElevationDeterminationMethod { get; set; }
        public int? MagneticVariation { get; set; }
        public int? MagneticVariationEpochYear { get; set; }
        public int? TPA { get; set; }
        public string SectionalName { get; set; }
        public int? DistanceFromCBDInNm { get; set; }
        public CompassPoint? DirectionFromCBD { get; set; }
        public string BoundaryArtccIdentifier { get; set; }
        public string BoundaryArtccFaaComputerIdentifier { get; set; }
        public string BoundaryArtccName { get; set; }
        public string ResponsibleArtccIdentifier { get; set; }
        public string ResponsibleArtccFaaComputerIdentifier { get; set; }
        public string ResponsibleArtccName { get; set; }
        public bool TieInFssOnFacility { get; set; }
        public string TieInFssIdentifier { get; set; }
        public string TieInFssName { get; set; }
        [Phone]
        public string LocalFssPhoneNumber { get; set; }
        [Phone]
        public string TollFreeFssPhoneNumber { get; set; }
        public string AlternateFssIdentifier { get; set; }
        public string AlternateFssName { get; set; }
        [Phone]
        public string TollFreeAlternateFssNumber { get; set; }
        public string NotamAndWeatherFacilityIdentifier { get; set; }
        public bool NotamDAvailableAtAirport { get; set; }
        public List<ILandingStructure> LandingStructures { get; set; } = new List<ILandingStructure>();
    }
}
