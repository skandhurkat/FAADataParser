﻿using System;

using FAADataParser.NASR.Aff;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FAADataParserTests.NASR.Aff
{
    [TestClass]
    public class TestAff1
    {
        [TestMethod]
        public void TestAff1ARSRNoLatLong()
        {
            string input = @"AFF1ZAB ALBUQUERQUE                             AMARILLO                                                                        ARSR 03/26/2020TEXAS                         TX                                                  KZAB                         ";
            Assert.IsTrue(Aff1.TryParse(input, out Aff1 aff1));
            Assert.AreEqual("ZAB", aff1.ArtccIdent);
            Assert.AreEqual("ALBUQUERQUE", aff1.ArtccName);
            Assert.AreEqual("AMARILLO", aff1.SiteLocation);
            Assert.IsNull(aff1.AltName);
            Assert.AreEqual(FacilityType.AirRouteSurveillanceRadar, aff1.FacilityType);
            Assert.AreEqual(new DateTime(2020, 3, 26), aff1.EffectiveDate);
            Assert.AreEqual("TEXAS", aff1.StateName);
            Assert.AreEqual("TX", aff1.StateCode);
            Assert.IsNull(aff1.Latitude);
            Assert.IsNull(aff1.Longitude);
            Assert.AreEqual("KZAB", aff1.IcaoId);
        }
        [TestMethod]
        public void TestAff1SecraNoLatLong()
        {
            string input = @"AFF1ZAN ANCHORAGE                               BIORKA ISLAND                                                                   SECRA03/26/2020ALASKA                        AK                                                  PAZA                         ";
            Assert.IsTrue(Aff1.TryParse(input, out Aff1 aff1));
            Assert.AreEqual("ZAN", aff1.ArtccIdent);
            Assert.AreEqual("ANCHORAGE", aff1.ArtccName);
            Assert.AreEqual("BIORKA ISLAND", aff1.SiteLocation);
            Assert.IsNull(aff1.AltName);
            Assert.AreEqual(FacilityType.SecondaryRadar, aff1.FacilityType);
            Assert.AreEqual(new DateTime(2020, 3, 26), aff1.EffectiveDate);
            Assert.AreEqual("ALASKA", aff1.StateName);
            Assert.AreEqual("AK", aff1.StateCode);
            Assert.IsNull(aff1.Latitude);
            Assert.IsNull(aff1.Longitude);
            Assert.AreEqual("PAZA", aff1.IcaoId);
        }
    }
}
