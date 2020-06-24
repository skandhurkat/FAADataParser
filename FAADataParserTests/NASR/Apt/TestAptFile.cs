using System.IO;
using System.IO.Compression;
using System.Net;

using FAADataParser;
using FAADataParser.NASR.Apt;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FAADataParserTests.NASR.Apt
{
    [TestClass]
    public class TestAptFile
    {
        [TestInitialize]
        public void Setup()
        {
            (bool found, System.DateTime cycle, bool fiftySixDay) currentCycle = Cycle.GetCurrentCycle();
            Assert.IsTrue(currentCycle.found);
            string fileName = string.Format("APT-{0}.txt", currentCycle.cycle.ToString("yyyy-MM-dd"));
            if (!File.Exists(fileName))
            {
                var client = new WebClient();
                client.DownloadFile(string.Format("https://nfdc.faa.gov/webContent/28DaySub/{0}/APT.zip", currentCycle.cycle.ToString("yyyy-MM-dd")), zipFileName);
                using (ZipArchive archive = ZipFile.OpenRead(zipFileName))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == "APT.txt")
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
        public void TestParseAptFile() => _ = AptFileParser.ParseFile(fileStream);

        private FileStream fileStream;
        private static readonly string zipFileName = "APT.zip";
    }
}
