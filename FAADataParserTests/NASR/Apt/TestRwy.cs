using System.Collections.Generic;

using FAADataParser.NASR.Apt;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FAADataParserTests.NASR.Apt
{
    [TestClass]
    public class TestRwy
    {
        [TestMethod]
        public void TestKBFI14L32R()
        {
            string recordString = @"RWY26396.*A   WA14L/32R 3709 100ASPH-F      GRVD 71 /F/A/X/TMED  14L150          NBSC  G47-32-16.8657N 171136.8657N122-18-26.8569W440306.8569W   17.4 393.0047-32-14.7266N 171134.7266N122-18-25.0488W440305.0488W   17.5 250   17.6P2L              Y                 A(V) 50                 32R330          YBSC  G47-31-45.0967N 171105.0967N122-18-00.0020W440280.0020W   17.1 393.0047-31-48.3086N 171108.3086N122-18-02.7110W440282.7110W   17.3 375   17.5P2L              Y  TREE           A(V) 22  210 5000625R   3RD PARTY SURVEY02/02/2016 120.0 250.0 550.01109.0         3RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/2016                                                                                                                                                                 3RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/2016                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
            Assert.AreEqual("26396.*A", rwy.SiteNumber);
            Assert.AreEqual("14L/32R", rwy.RwyIdentification);
            Assert.AreEqual(3709, rwy.LengthInFeet);
            Assert.AreEqual(100, rwy.WidthInFeet);
            CollectionAssert.AreEqual(new List<RunwaySurfaceType> { RunwaySurfaceType.Asphalt }, rwy.RunwaySurface.surfaceTypes);
            Assert.IsNotNull(rwy.RunwaySurface.surfaceCondition);
            Assert.AreEqual(RunwaySurfaceCondition.Fair, (RunwaySurfaceCondition)rwy.RunwaySurface.surfaceCondition);
            Assert.AreEqual("14L", rwy.BaseEndIdentifier);
            Assert.AreEqual(150, rwy.BaseEndTrueAlignment);
            Assert.IsFalse((bool)rwy.BaseEndRightTraffic);
            Assert.AreEqual(171136.8657m, rwy.BaseEndLatitude);
            Assert.AreEqual(-440306.8569m, rwy.BaseEndLongitude);
            Assert.AreEqual(17.4, rwy.BaseEndElevation);
            Assert.AreEqual(171134.7266m, rwy.BaseEndDisplacedThresholdLatitude);
            Assert.AreEqual(-440305.0488m, rwy.BaseEndDisplacedThresholdLongitude);
            Assert.AreEqual(17.5, rwy.BaseEndDisplacedThresholdElevation);
            Assert.AreEqual(17.6, rwy.BaseEndTouchdownZoneElevation);
            Assert.AreEqual("32R", rwy.ReciprocalEndIdentifier);
            Assert.AreEqual(330, rwy.ReciprocalEndTrueAlignment);
            Assert.IsTrue((bool)rwy.ReciprocalEndRightTraffic);
            Assert.AreEqual(171105.0967m, rwy.ReciprocalEndLatitude);
            Assert.AreEqual(-440280.0020m, rwy.ReciprocalEndLongitude);
            Assert.AreEqual(17.1, rwy.ReciprocalEndElevation);
            Assert.AreEqual(171108.3086m, rwy.ReciprocalEndDisplacedThresholdLatitude);
            Assert.AreEqual(-440282.7110m, rwy.ReciprocalEndDisplacedThresholdLongitude);
            Assert.AreEqual(17.3, rwy.ReciprocalEndDisplacedThresholdElevation);
            Assert.AreEqual(17.5, rwy.ReciprocalEndTouchdownZoneElevation);
        }
        [TestMethod]
        public void TestPADK0523()
        {
            string recordString = @"RWY50009.*A   AK05/23   7790 200ASPH-G      GRVD 49 /R/B/X/THIGH 05 061          YNPI  G51-52-42.0225N 186762.0225N176-39-26.9889W635966.9889W   19.5                                                                           19.5                  NNHILL           B(V) 10                 23 241LOC/GS     PIR  G51-53-19.7682N 186799.7682N176-37-38.8664W635858.8664W   17.3 533.50                                                                    17.5P4R      MALS    YNN               PIR  50                 AVN             06/18/2003  80.0 145.0 325.0 770.0         AVN             07/17/2011AVN             07/17/2011                                                    3RD PARTY SURVEY07/17/2011 7790 7790 6790 6190                                                                                                                                             AVN             07/17/2011AVN             07/17/2011                                                    3RD PARTY SURVEY07/17/2011 7790 7790 6790 6790                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
            Assert.AreEqual("50009.*A", rwy.SiteNumber);
            Assert.AreEqual("05/23", rwy.RwyIdentification);
            Assert.AreEqual(7790, rwy.LengthInFeet);
            Assert.AreEqual(200, rwy.WidthInFeet);
            CollectionAssert.AreEqual(new List<RunwaySurfaceType> { RunwaySurfaceType.Asphalt }, rwy.RunwaySurface.surfaceTypes);
            Assert.IsNotNull(rwy.RunwaySurface.surfaceCondition);
            Assert.AreEqual(RunwaySurfaceCondition.Good, (RunwaySurfaceCondition)rwy.RunwaySurface.surfaceCondition);
            Assert.AreEqual("05", rwy.BaseEndIdentifier);
            Assert.AreEqual(061, rwy.BaseEndTrueAlignment);
            Assert.IsTrue((bool)rwy.BaseEndRightTraffic);
            Assert.AreEqual(186762.0225m, rwy.BaseEndLatitude);
            Assert.AreEqual(-635966.9889m, rwy.BaseEndLongitude);
            Assert.AreEqual(19.5, rwy.BaseEndElevation);
            Assert.IsNull(rwy.BaseEndDisplacedThresholdLatitude);
            Assert.IsNull(rwy.BaseEndDisplacedThresholdLongitude);
            Assert.IsNull(rwy.BaseEndDisplacedThresholdElevation);
            Assert.AreEqual(19.5, rwy.BaseEndTouchdownZoneElevation);
            Assert.AreEqual("23", rwy.ReciprocalEndIdentifier);
            Assert.AreEqual(241, rwy.ReciprocalEndTrueAlignment);
            Assert.IsFalse((bool)rwy.ReciprocalEndRightTraffic);
            Assert.AreEqual(186799.7682m, rwy.ReciprocalEndLatitude);
            Assert.AreEqual(-635858.8664m, rwy.ReciprocalEndLongitude);
            Assert.AreEqual(17.3, rwy.ReciprocalEndElevation);
            Assert.IsNull(rwy.ReciprocalEndDisplacedThresholdLatitude);
            Assert.IsNull(rwy.ReciprocalEndDisplacedThresholdLongitude);
            Assert.IsNull(rwy.ReciprocalEndDisplacedThresholdElevation);
            Assert.AreEqual(17.5, rwy.ReciprocalEndTouchdownZoneElevation);
        }
        [TestMethod]
        public void TestZ130119()
        {
            string recordString = @"RWY50017.*A   AK01/19   3300  60GRAVEL-G                    MED  01               NSTD  60-54-35.1700N 219275.1700N161-29-50.8500W581390.8500W   22.8                                                                           22.8                    BRUSH          A(V)  2    5   10100L   19               NSTD  60-55-04.2600N 219304.2600N161-29-21.1200W581361.1200W   22.8                                                                           22.8                    BRUSH          A(V)  6    4   2490L/R  STATE           12/02/2013                                 STATE           12/05/2013STATE           12/05/2013                                                    STATE           12/05/2013                                                                                                                                                                 STATE           12/05/2013STATE           12/05/2013                                                    STATE           12/05/2013                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
            Assert.AreEqual("50017.*A", rwy.SiteNumber);
            Assert.AreEqual("01/19", rwy.RwyIdentification);
            Assert.AreEqual(3300, rwy.LengthInFeet);
            Assert.AreEqual(60, rwy.WidthInFeet);
            CollectionAssert.AreEqual(new List<RunwaySurfaceType> { RunwaySurfaceType.Gravel }, rwy.RunwaySurface.surfaceTypes);
            Assert.IsNotNull(rwy.RunwaySurface.surfaceCondition);
            Assert.AreEqual(RunwaySurfaceCondition.Good, (RunwaySurfaceCondition)rwy.RunwaySurface.surfaceCondition);
            Assert.AreEqual("01", rwy.BaseEndIdentifier);
            Assert.IsNull(rwy.BaseEndTrueAlignment);
            Assert.IsFalse((bool)rwy.BaseEndRightTraffic);
            Assert.AreEqual(219275.1700m, rwy.BaseEndLatitude);
            Assert.AreEqual(-581390.8500m, rwy.BaseEndLongitude);
            Assert.AreEqual(22.8, rwy.BaseEndElevation);
            Assert.IsNull(rwy.BaseEndDisplacedThresholdLatitude);
            Assert.IsNull(rwy.BaseEndDisplacedThresholdLongitude);
            Assert.IsNull(rwy.BaseEndDisplacedThresholdElevation);
            Assert.AreEqual(22.8, rwy.BaseEndTouchdownZoneElevation);
            Assert.AreEqual("19", rwy.ReciprocalEndIdentifier);
            Assert.IsNull(rwy.ReciprocalEndTrueAlignment);
            Assert.IsFalse((bool)rwy.ReciprocalEndRightTraffic);
            Assert.AreEqual(219304.2600m, rwy.ReciprocalEndLatitude);
            Assert.AreEqual(-581361.1200m, rwy.ReciprocalEndLongitude);
            Assert.AreEqual(22.8, rwy.ReciprocalEndElevation);
            Assert.IsNull(rwy.ReciprocalEndDisplacedThresholdLatitude);
            Assert.IsNull(rwy.ReciprocalEndDisplacedThresholdLongitude);
            Assert.IsNull(rwy.ReciprocalEndDisplacedThresholdElevation);
            Assert.AreEqual(22.8, rwy.ReciprocalEndTouchdownZoneElevation);
        }
        [TestMethod]
        public void TestKKIEW()
        {
            string recordString = @"RWY50017.1*C  AKE/W     5000 300WATER                            E               NNONE                                                            0.0                                                                                       N        NNN                    50                 W               NNONE                                                            0.0                                                                                       N        NNN                    50                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));

        }
        [TestMethod]
        public void TestKQA()
        {
            string recordString = @"RWY50022.*C   AKE/W    100001000WATER-E                          E               NNONE                                                                                                                                                      N        NNN                                       W               NNONE                                                                                                                                                      N        NNN                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestAK82Heliport()
        {
            string recordString = @"RWY50033.2*H  AKH1       100  90TRTD                             H1              NNSTD  61-09-22.0700N 220162.0700N149-47-32.2800W539252.2800W  235.0                                                                                                 N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void Test2AK01634()
        {
            string recordString = @"RWY50037.01*A AK16/34   1400  60DIRT                             16                                                                                                                                                                                                    A(V)                    34                                                                                                                                                                                                    A(V)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestBLG0119()
        {
            string recordString = @"RWY50059.95*A AKH1        60  60CONC                        PERI H1                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void Test2AK50624()
        {
            string recordString = @"RWY50310.4*A  AK06/24   1700  60GRAVEL                           06              NNONE                                                           15.0                                                                                       N        NNNTREES               18       500       24              NNONE                                                                                                                                                      N        NNNTREES               15       500                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestAA44H1()
        {
            string recordString = @"RWY50385.02*H AKH1        55  55ROOF-TOP                    PERI H1                     58-19-43.3300N 209983.3300N134-27-56.5600W484076.5600W  296.0                                                                                                   BLDG                                                                                                                                                                                                                                                                 OWNER           09/01/2008                                 FAA-EST IMAGERY 09/13/2014OWNER           09/01/2008                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestKDK0220()
        {
            string recordString = @"RWY50423.*A   AK02/20   2475  40ASPH-TRTD-F                      02              YNSTD P                                                                                                                                  240                           TREE           A(V) 11   80 1090190R   20              NNSTD P                                                                                                                                                                TREES          A(V) 25   75 2112125L/R                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestLKK0725()
        {
            string recordString = @"RWY50437.*A   AK07/25   4400 110GRVL-DIRT-F                      07 086          N      58-57-52.3250N 212272.3250N155-06-26.0340W558386.0340W  700.0                                                                          717.0                    BRUSH          A(V)  0   15    0B      25 266          N      58-57-55.7110N 212275.7110N155-05-02.3760W558302.3760W  715.0                                                                                                   BRUSH          A(V)  0   15    0B      FAA             06/12/2012                                 FAA             06/21/2012                                                                                                                                                                                                                                                                         FAA             06/21/2012                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void Test26A0927()
        {
            string recordString = @"RWY00135.*A   AL09/27   4023  80ASPH-G                      MED  09              NNPI  G33-17-03.1100N 119823.1100N085-48-55.9700W308935.9700W 1065.9       33-17-03.1100N 119823.1100N085-48-53.6100W308933.6100W        200                        Y  POLE           A(V) 21   35  96918L    27              NNPI  G33-17-03.1100N 119823.1100N085-48-08.5800W308888.5800W 1057.3                                                                                                Y  TREE           A(V) 24   46 13172R     ADO             08/23/2012  20.0                           FAA-EST IMAGERY 04/25/2007ADO             08/23/2012ADO             08/23/2012                                                     4023 4023                                                                                                                                                       FAA-EST IMAGERY 04/25/2007AIP             08/23/2012                                                                               3823 4023                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestAL53H1()
        {
            string recordString = @"RWY00455.01*H ALH1        40  40PFC                              H1                     34-44-50.2300N 125090.2300N087-40-39.9800W315639.9800W                                                                                                                                                                                                                                                                                                                                                                                                                                          FAA-EST IMAGERY 11/29/2013                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void Test11V1432()
        {
            string recordString = @"RWY02627.2*A  CO14/32   2400  65TURF-DIRT-G                      14              N      40-20-02.5800N 145202.5800N104-37-05.6400W376625.6400W                                                                                                          ROAD           A(V)  1   10   100B     32              N                                                                                                                                                                                     A(V) 50          0                                                                 STATE           12/20/2004                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestLHDSENW()
        {
            string recordString = @"RWY50037.*A   AKSE/NW   1369 150WATER                            NW                                                                                                                                                                                                                            SE                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestDE39H1()
        {
            string recordString = @"RWY02999.24*H DEH1        50  50ROOF-TOP-E                  PERI H1               BSC  F39-45-04.0000N 143104.0000N075-33-02.0000W271982.0000W  200.0                                                                          200.0                                                                                                                                                                                                                                                                                         OWNER           11/20/2017                                 OWNER           11/20/2017OWNER           11/20/2017                                                    OWNER           11/20/2017                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestHFS0624()
        {
            string recordString = @"RWY51536.*A   HI06/24   3000 200CORAL                            06                                                                                                                                                                                                                            24                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestHMN0422()
        {
            string recordString = @"RWY14525.*A   NM04/22  10578 300PEM              56 /R/A/W/THIGH 04              NNPI  G32-50-48.4900N 118248.4900N106-07-57.4600W382077.4600W 4058.7                                                                         4058.7        N        NNN                                       22 228ILS       NNPI  G32-51-58.7600N 118318.7600N106-06-25.6100W381985.6100W 4082.8 543.00                                                                  4082.8P4L     NALSF1   NNN                                       MILITARY        09/14/2017                                 MILITARY        11/28/2012FAA             03/16/2018                                                    FAA             03/16/2016                                                                                                                                                                 MILITARY        10/27/2011MILITARY        09/14/2017                                                    MILITARY        09/14/2017                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
        [TestMethod]
        public void TestVOS0624()
        {
            string recordString = @"RWY23184.*A   TN06/24   3700  50ASPH-G                      MED  06              NBSC  P                                                                                                                                            P2L                 TREE           A(V)  7   24  38854L    24              NBSC  P35-12-25.6000N 126745.6000N085-53-35.4200W309215.4200W 1953.3                                                                     200       P2L                 TREE           A(V) 15  150 2524298L   FAA             12/27/2004  15.0  25.0                                                                                                                                                                                                                                                                                                                        NACO            07/23/2002NACO            07/23/2002                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ";
            Assert.IsTrue(Rwy.TryParse(recordString, out Rwy rwy));
        }
    }
}
