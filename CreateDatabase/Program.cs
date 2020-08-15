using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace CreateDatabase
{
    internal class Program
    {
        public static async Task Main()
        {
            var db = new FAADatabase.Database();
            string zipFileName = "APT.zip";
            (bool found, System.DateTime cycle, bool fiftySixDay) currentCycle = FAADataParser.Cycle.GetCurrentCycle();
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
            FileStream fileStream = File.OpenRead(fileName);
            List<FAADataParser.NASR.Apt.Airport> airports = FAADataParser.NASR.Apt.AptFileParser.ParseFile(fileStream);
            var listAwaits = new List<Task>();
            foreach (FAADataParser.NASR.Apt.Airport airport in airports)
            {
                listAwaits.Add(db.InsertAirportAsync(airport));
            }
            await Task.WhenAll(listAwaits);
        }
    }
}
