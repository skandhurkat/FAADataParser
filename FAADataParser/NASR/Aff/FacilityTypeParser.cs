namespace FAADataParser.NASR.Aff
{
    public enum FacilityType
    {
        AirRouteSurveillanceRadar,
        AirRouteTrafficControlCentre,
        CentreRadarApproachControlFacility,
        RemoteCommunicationsAirGround,
        SecondaryRadar
    };

    static class FacilityTypeParser
    {
        public static bool ParseFacilityType(string value, out object facilityType)
        {
            facilityType = null;
            switch (value)
            {
                case "ARSR": facilityType = FacilityType.AirRouteSurveillanceRadar; break;
                case "ARTCC": facilityType = FacilityType.AirRouteTrafficControlCentre; break;
                case "CERAP": facilityType = FacilityType.CentreRadarApproachControlFacility; break;
                case "RCAG": facilityType = FacilityType.RemoteCommunicationsAirGround; break;
                case "SECRA": facilityType = FacilityType.SecondaryRadar; break;
                default: return false;
            }
            return true;
        }
    }
}
