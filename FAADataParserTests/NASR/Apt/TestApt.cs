using System;

using FAADataParser.NASR.Apt;
using FAADataParser.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FAADataParserTests.NASR.Apt
{
    [TestClass]
    public class TestApt
    {
        [TestMethod]
        public void TestParseKBFI()
        {
            string record = @"APT26396.*A   AIRPORT      BFI 04/23/2020ANMSEA WAWASHINGTON          KING                 WASEATTLE                                 BOEING FIELD/KING COUNTY INTL                     PUPUKING COUNTY                        516 THIRD AVE                                                           SEATTLE, WA 98104                              (206) 296-7380JOHN PARROTT, AAE                  7277 PERIMETER ROAD SOUTH                                               SEATTLE, WA 98108-3844                         (206) 296-733447-31-47.9000N 171107.9000N122-18-07.0000W440287.0000WE   21.6S15E2020    SEATTLE                       04S    634ZSE ZCSSEATTLE                       ZSE ZCSSEATTLE                       NSEA SEATTLE                                       1-800-WX-BRIEF                                                    BFI Y11/1938O IV A S 01/1974 NGPY3  NOT ANALYZED YNNYF F06262019        100LLA                                  MAJORMAJORHIGH/LOWHIGH/LOWSEE RMKSS-SR  Y122.950       N   CG Y 229040088026001000000010142      02791304953709450000117601/01/20193RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/2016 HGR,TIE     AFRT,AMB,AVNCS,CARGO,CHTR,INSTR,RNTL,SALES,SURV                        Y-LKBFI   N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(record, out FAADataParser.NASR.Apt.Apt apt));
            Assert.AreEqual("26396.*A", apt.SiteNumber);
            Assert.AreEqual(LandingFacilityType.Airport, apt.LandingFacilityType);
            Assert.AreEqual("BFI", apt.LocationIdentifier);
            Assert.AreEqual(new DateTime(2020, 4, 23), apt.InformationEffectiveDate);
            Assert.AreEqual("SEA", apt.FAAFieldOffice);
            Assert.AreEqual("WA", apt.StatePostOfficeCode);
            Assert.AreEqual("WASHINGTON", apt.StateName);
            Assert.AreEqual("KING", apt.CountyName);
            Assert.AreEqual("WA", apt.CountyStatePostOfficeCode);
            Assert.AreEqual("SEATTLE", apt.CityName);
            Assert.AreEqual("BOEING FIELD/KING COUNTY INTL", apt.FacilityName);
            Assert.AreEqual(AirportOwnershipType.PubliclyOwned, apt.AirportOwnershipType);
            Assert.AreEqual(FacilityUse.Public, apt.FacilityUse);
            Assert.AreEqual("KING COUNTY", apt.OwnersName);
            Assert.AreEqual("516 THIRD AVE", apt.OwnersAddress1);
            Assert.AreEqual("SEATTLE, WA 98104", apt.OwnersAddress2);
            Assert.AreEqual("+1 206-296-7380", apt.OwnersPhone);
            Assert.AreEqual("JOHN PARROTT, AAE", apt.ManagersName);
            Assert.AreEqual("7277 PERIMETER ROAD SOUTH", apt.ManagersAddress1);
            Assert.AreEqual("SEATTLE, WA 98108-3844", apt.ManagersAddress2);
            Assert.AreEqual("+1 206-296-7334", apt.ManagersPhone);
            Assert.AreEqual(171107.9000m, apt.Latitude);
            Assert.AreEqual(-440287.0000m, apt.Longitude);
            Assert.AreEqual(SurveyMethod.Estimated, apt.ReferencePointDeterminationMethod);
            Assert.AreEqual(21.6m, apt.Elevation);
            Assert.AreEqual(SurveyMethod.Surveyed, apt.ElevationDeterminationMethod);
            Assert.AreEqual(-15, apt.MagneticVariation);
            Assert.AreEqual(2020, apt.MagneticVariationEpochYear);
            Assert.IsNull(apt.TPA);
            Assert.AreEqual("SEATTLE", apt.SectionalName);
            Assert.AreEqual(04, apt.DistanceFromCBDInNm);
            Assert.AreEqual(CompassPoint.South, apt.DirectionFromCBD);
            Assert.AreEqual("ZSE", apt.BoundaryArtccIdentifier);
            Assert.AreEqual("ZCS", apt.BoundaryArtccFaaComputerIdentifier);
            Assert.AreEqual("SEATTLE", apt.BoundaryArtccName);
            Assert.AreEqual("ZSE", apt.ResponsibleArtccIdentifier);
            Assert.AreEqual("ZCS", apt.ResponsibleArtccFaaComputerIdentifier);
            Assert.AreEqual("SEATTLE", apt.ResponsibleArtccName);
            Assert.IsFalse((bool)apt.TieInFssOnFacility);
            Assert.AreEqual("SEA", apt.TieInFssIdentifier);
            Assert.AreEqual("SEATTLE", apt.TieInFssName);
            Assert.IsNull(apt.LocalFssPhoneNumber);
            Assert.AreEqual("+1 800-992-7433", apt.TollFreeFssPhoneNumber);
            Assert.IsNull(apt.AlternateFssIdentifier);
            Assert.IsNull(apt.AlternateFssName);
            Assert.IsNull(apt.TollFreeAlternateFssNumber);
            Assert.AreEqual("BFI", apt.NotamAndWeatherFacilityIdentifier);
            Assert.IsTrue((bool)apt.NotamDAvailableAtAirport);
        }
        [TestMethod]
        public void TestParseKKI()
        {
            string recordString = @"APT50017.1*C  SEAPLANE BASEKKI 06/18/2020AALNONEAKALASKA              BETHEL               AKAKIACHAK                                AKIACHAK                                          PUPUPUBLIC DOMAIN                                                                                                                                                           NONE                                                                                                                                                                    60-54-28.3130N 219268.3130N161-26-06.2780W581166.2780WE   18.0E19E1985    MC GRATH                      00S       ZAN ZANANCHORAGE                     ZAN ZANANCHORAGE                     NENA KENAI                         907-283-7211    1-866-864-1737                                                    ENA Y07/1966O                       NO OBJECTION NNNNS C06302017                                                NONE NONE NONE    NONE                  N       122.900N      N                                              000010      12/31/2016                                                                                                                                        N         N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParse7AK()
        {
            string recordString = @"APT50022.1*A  AIRPORT      7AK 04/23/2020AALNONEAKALASKA              ALEUTIANS EAST       AKAKUTAN                                  AKUTAN                                            PUPUALASKA DOT&PF SOUTHCOAST REGION    PO BOX 112506                                                           JUNEAU, AK 99811-2506                          (907) 465-1779DALE RUCKMAN                       P.O. BOX 920525                                                         DUTCH HARBOR, AK 99692                         (907) 581-178654-08-40.6000N 194920.6000N165-36-14.7900W596174.7900WE  129.3S10E2015    DUTCH HARBOR                  06E    369ZAN ZANANCHORAGE                     ZAN ZANANCHORAGE                      CDB COLD BAY                      907-532-2466    1-800-478-7250  ENA KENAI                         1-866-864-1737  7AK Y07/2012O                                          C07132019                                                                          SEE RMKSS-SR  N       122.900Y   CG   000000000000000000000            000624                  12/31/2018STATE           11/17/20143RD PARTY SURVEY10/03/2012                                                                                    Y-LPAUT   N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParseAA06()
        {
            string recordString = @"APT50033.02*A AIRPORT      AA0604/23/2020AALNONEAKALASKA              ALEUTIAN ISLANDS     AKANCHORAGE                               SIXMILE LAKE                                      PRPRUS AIR FORCE                       10471  20TH  ST STE 160                                                 ELMENDORF  AFB, AK 99506                         907-552-2107 MAJOR ROBERT PECK                 10471  20TH  ST STE 160                                                 ELMENDORF  AFB, AK 99506                         907-552-210761-17-23.0000N 220643.0000N149-48-22.0000W539302.0000WE   85.0E           ANCHORAGE                     02NE    80ZAN ZANANCHORAGE                     ZAN ZANANCHORAGE                      ENA KENAI                         907-283-7211    1-866-864-1737                                                         12/2008O                       NO OBJECTION     2 N        12232008                                                                                N       122.900                                                                            OWNER           03/16/2009OWNER           03/16/2009                                                                                    Y         N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParse99AA()
        {
            string recordString = @"APT50033.24*H HELIPORT     99AA04/23/2020AALNONEAKALASKA              ANCHORAGE            AKANCHORAGE                               AVIATOR HOTEL ANCHORAGE                           PRPRH.P. HOLDINGS INC.                 239 W. 4TH AVE.                                                         ANCHORAGE, AK 99501                              907-440-9000RANDY COMER                        239 W. 4TH AVE.                                                         ANCHORAGE, AK 99501                              907-793-555561-13-08.0600N 220388.0600N149-53-11.3300W539591.3300WE  123.0                                                    ZAN ZANANCHORAGE                     ZAN ZANANCHORAGE                      ENA KENAI                         907-283-7211    1-866-864-1737                                                         04/2016O                       CONDITIONAL        N                                                                                                N                                                                                                                                                                                                                                            N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParse4AA4()
        {
            string recordString = @"APT50320.16*H HELIPORT     4AA404/23/2020AALNONEAKALASKA              KENAI PENINSULA      AKHOMER                                   SOUTH PENINSULA HOSPITAL                          PRPRKENAI PENINSULA BOROUGH            144 N BINKLEY ST                                                        SOLDOTNA, AK 99669                               907-262-4441SOUITH PENINSULA HOSPITAL          4300 BARTLETT ST                                                        HOMER, AK 99603                                  907-235-035159-39-08.1400N 214748.1400N151-33-00.0000W545580.0000WE  370.0E           KODIAK                        00        ZAN ZANANCHORAGE                     ZAN ZANANCHORAGE                      ENA KENAI                         907-283-7211    1-866-864-1737                                                         11/2009O                       CONDITIONAL      2 N        01242020                                                                  SEE RMK       N123.075           CGY Y                                                         12/05/2007OWNER           09/22/2009OWNER           09/22/2009                                                                                    Y         N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParseAK53()
        {
            string recordString = @"APT50449.6*A  AIRPORT      AK5304/23/2020AALNONEAKALASKA              BETHEL               AKLIME VILLAGE                            STONY MOUNTAIN LODGE                              PRPRNORTHWARD BOUND LLC                12902 NORA DRIVE                                                        ANCHORAGE, AK 99515                              907-227-6774KRIS OLDHAM                        12902 NORA DRIVE                                                        ANCHORAGE, AK 99515                              907-227-677461-15-25.5210N 220525.5210N153-47-52.3790W553672.3790WE 1197.0E15E        MC GRATH                      48E       ZAN ZANANCHORAGE                     ZAN ZANANCHORAGE                      ENA KENAI                         907-283-7211    1-866-864-1737                                                         01/2017O                       NO OBJECTION       N        03062020                                                                                N              N        002      001                                                       OWNER           01/23/2017OWNER           01/23/2017                                                                                    N         N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParseAZ93()
        {
            string recordString = @"APT00825.3*H  HELIPORT     AZ9304/23/2020AWPPHX AZARIZONA             MARICOPA             AZWICKENBURG                              TOYOTA ARIZONA PROVING GROUND                     PRPRTOYOTA ENGINEERING AND MANU. TEMA  30700 W PATTON RD                                                       WITTMANN, AZ 85361                             (623) 546-5300DAN HUNT                           30700 W PATTON RD                                                       WITTMANN, AZ 85361                             (623) 546-536433-44-34.3900N 121474.3900N112-46-06.6900W405966.6900WE 1660.0E   2016    PHOENIX                       14SW      ZAB ZCAALBUQUERQUE                   ZAB ZCAALBUQUERQUE                   NPRC PRESCOTT                                      1-800-WX-BRIEF                                                        N09/1994O                       CONDITIONAL      2 N        02062018                                                                                N              N                                                                                                                                                                                                                             N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParseCN53()
        {
            string recordString = @"APT01641.12*H HELIPORT     CN5304/23/2020AWPSFO CACALIFORNIA          KINGS                CAHANFORD                                 KINGS COUNTY HOUSTON AVE                          PUPRKINGS COUNTY                       1400 W. LACEY BLVD.                                                     HANFORD, CA 93230                                559-362-3201CLAY SMITH/FIRE CHIEF              280 N. CAMPUS DR.                                                       HANFORD, CA 93230                                559-582-321136-17-59.5300N 130679.5300N119-35-41.4700W430541.4700WE  239.0E           SAN FRANCISCO                 02        ZOA ZCOOAKLAND                       ZOA ZCOOAKLAND                        RIU RANCHO MURIETA                                1-800-WX-BRIEF                                                         12/2007O                       CONDITIONAL      2 N        02252019                                                                  SEE RMK       N                  CGY           000                                                       FAA-EST         11/21/2016OWNER           06/25/2007                                                                                              N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
        [TestMethod]
        public void TestParseFL77()
        {
            string recordString = @"APT03349.1*A  AIRPORT      FL7704/23/2020ASOORL FLFLORIDA             COLLIER              FLMILES CITY                              CALUSA RANCH                                      PRPRCALUSA RANCH AIRPORT, LLC          662 NOTINGHAM FOREST CIRCLE                                             ST JOHNS, FL 32259                             (904) 703-5040TODD CLARK                         662 NOTINGHAM FOREST CIRCLE                                             ST JOHNS, FL 32259                                          -26-03-16.3280N 093796.3280N081-04-04.2520W291844.2520WE   15.0S02W1985    MIAMI                         15SE   640ZMA ZCRMIAMI                         ZMA ZCRMIAMI                         NMIA MIAMI                                         1-800-WX-BRIEF                                                        N11/1975O                       CONDITIONAL      2 N        02042019                                                                                N              N      N 005                                                                                                                                                                                                                  N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(FAADataParser.NASR.Apt.Apt.TryParse(recordString, out FAADataParser.NASR.Apt.Apt apt));
            // TODO: Actually test this
        }
    }
}
