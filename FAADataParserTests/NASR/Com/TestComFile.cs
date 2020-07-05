using System.IO;
using System.IO.Compression;
using System.Net;

using FAADataParser;
using FAADataParser.NASR.Com;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FAADataParserTests.NASR.Com
{
    [TestClass]
    public class TestComFile
    {
        [TestInitialize]
        public void Setup()
        {
            (bool found, System.DateTime cycle, bool fiftySixDay) currentCycle = Cycle.GetCurrentCycle();
            Assert.IsTrue(currentCycle.found);
            string fileName = string.Format("COM-{0}.txt", currentCycle.cycle.ToString("yyyy-MM-dd"));
            if (!File.Exists(fileName))
            {
                var client = new WebClient();
                client.DownloadFile(string.Format("https://nfdc.faa.gov/webContent/28DaySub/{0}/COM.zip", currentCycle.cycle.ToString("yyyy-MM-dd")), zipFileName);
                using (ZipArchive archive = ZipFile.OpenRead(zipFileName))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == "COM.txt")
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
        public void TestParseComFile() => _ = ComFileParser.ParseFile(fileStream);

        private FileStream fileStream;
        private static readonly string zipFileName = "COM.zip";
    }
}
