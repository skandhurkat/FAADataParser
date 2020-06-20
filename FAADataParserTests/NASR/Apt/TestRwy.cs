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
            Assert.AreEqual("14L", rwy.BaseEndIdentifier);
            Assert.AreEqual(150, rwy.BaseEndTrueAlignment);
            Assert.IsFalse(rwy.BaseEndRightTraffic);
            Assert.AreEqual(171136.8657m, rwy.BaseEndLatitude);
            Assert.AreEqual(-440306.8569m, rwy.BaseEndLongitude);
            Assert.AreEqual(17.4, rwy.BaseEndElevation);
            Assert.AreEqual(171134.7266m, rwy.BaseEndDisplacedThresholdLatitude);
            Assert.AreEqual(-440305.0488m, rwy.BaseEndDisplacedThresholdLongitude);
            Assert.AreEqual(17.5, rwy.BaseEndDisplacedThresholdElevation);
            Assert.AreEqual(17.6, rwy.BaseEndTouchdownZoneElevation);
            Assert.AreEqual("32R", rwy.ReciprocalEndIdentifier);
            Assert.AreEqual(330, rwy.ReciprocalEndTrueAlignment);
            Assert.IsTrue(rwy.ReciprocalEndRightTraffic);
            Assert.AreEqual(171105.0967m, rwy.ReciprocalEndLatitude);
            Assert.AreEqual(-440280.0020m, rwy.ReciprocalEndLongitude);
            Assert.AreEqual(17.1, rwy.ReciprocalEndElevation);
            Assert.AreEqual(171108.3086m, rwy.ReciprocalEndDisplacedThresholdLatitude);
            Assert.AreEqual(-440282.7110m, rwy.ReciprocalEndDisplacedThresholdLongitude);
            Assert.AreEqual(17.3, rwy.ReciprocalEndDisplacedThresholdElevation);
            Assert.AreEqual(17.5, rwy.ReciprocalEndTouchdownZoneElevation);
        }
    }
}
