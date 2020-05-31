using System.IO;
using System.IO.Compression;
using System.Net;

using FAADataParser;
using FAADataParser.NASR.Aff;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FAADataParserTests.NASR.Aff
{
    [TestClass]
    public class TestAffFile
    {
        [TestInitialize]
        public void Setup()
        {
            (bool found, System.DateTime cycle, bool fiftySixDay) currentCycle = Cycle.GetCurrentCycle();
            Assert.IsTrue(currentCycle.found);
            string fileName = string.Format("AFF-{0}.txt", currentCycle.cycle.ToString("yyyy-MM-dd"));
            if (!File.Exists(fileName))
            {
                var client = new WebClient();
                client.DownloadFile(string.Format("https://nfdc.faa.gov/webContent/28DaySub/{0}/AFF.zip", currentCycle.cycle.ToString("yyyy-MM-dd")), zipFileName);
                using (ZipArchive archive = ZipFile.OpenRead(zipFileName))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == "AFF.txt")
                        {
                            entry.ExtractToFile(fileName);
                            break;
                        }
                    }
                }
                File.Delete(zipFileName);
            }
            fileStream = File.OpenRead(fileName);
        }

        [TestCleanup]
        public void Teardown()
        {
            fileStream.Close();
            fileStream = null;
        }

        [TestMethod]
        public void TestParseAffFile() => _ = AffFileParser.ParseFile(fileStream);

        private FileStream fileStream;
        private static readonly string zipFileName = "AFF.zip";
    }
}
