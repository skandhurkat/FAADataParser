using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FAADatabase.NASR;

using SQLite;

namespace FAADatabase
{
    public class Database
    {
        public async Task InsertAirportAsync(FAADataParser.NASR.Apt.Airport airport)
        {
            var dbAirport = new Airport
            {
                FAAComputerIdentifier = airport.FAAComputerIdentifier,
                LandingFacilityType = airport.LandingFacilityType,
                LocationIdentifier = airport.LocationIdentifier,
                InformationEffectiveDate = airport.InformationEffectiveDate,
                FacilityName = airport.FacilityName,
                AirportOwnershipType = airport.AirportOwnershipType,
                FacilityUse = airport.FacilityUse,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                Elevation = airport.Elevation,
                MagneticVariation = airport.MagneticVariation,
                MagneticVariationEpochYear = airport.MagneticVariationEpochYear,
                TPA = airport.TPA,
            };
            _ = await _database.InsertAsync(dbAirport);
            foreach (FAADataParser.NASR.Apt.ILandingStructure landingStructure in airport.LandingStructures)
            {
                if (landingStructure.GetType() == typeof(FAADataParser.NASR.Apt.Runway))
                {
                    var rwy = (FAADataParser.NASR.Apt.Runway)landingStructure;
                    var dbRwy = new Runway
                    {
                        AirportPrimaryKey = dbAirport.FAAComputerIdentifier,
                        Identification = rwy.Identification,
                        LengthInFeet = rwy.LengthInFeet,
                        WidthInFeet = rwy.WidthInFeet,
                        BaseEndIdentifier = rwy.BaseEndIdentifier,
                        BaseEndTrueAlignment = rwy.BaseEndTrueAlignment,
                        BaseEndRightTraffic = rwy.BaseEndRightTraffic,
                        BaseEndLatitude = rwy.BaseEndLatitude,
                        BaseEndLongitude = rwy.BaseEndLongitude,
                        BaseEndElevation = rwy.BaseEndElevation,
                        BaseEndDisplacedThresholdLatitude = rwy.BaseEndDisplacedThresholdLatitude,
                        BaseEndDisplacedThresholdLongitude = rwy.BaseEndDisplacedThresholdLongitude,
                        BaseEndDisplacedThresholdElevation = rwy.BaseEndDisplacedThresholdElevation,
                        BaseEndTouchdownZoneElevation = rwy.BaseEndTouchdownZoneElevation,
                        ReciprocalEndIdentifier = rwy.ReciprocalEndIdentifier,
                        ReciprocalEndTrueAlignment = rwy.ReciprocalEndTrueAlignment,
                        ReciprocalEndRightTraffic = rwy.ReciprocalEndRightTraffic,
                        ReciprocalEndLatitude = rwy.ReciprocalEndLatitude,
                        ReciprocalEndLongitude = rwy.ReciprocalEndLongitude,
                        ReciprocalEndElevation = rwy.ReciprocalEndElevation,
                        ReciprocalEndDisplacedThresholdLatitude = rwy.ReciprocalEndDisplacedThresholdLatitude,
                        ReciprocalEndDisplacedThresholdLongitude = rwy.ReciprocalEndDisplacedThresholdLongitude,
                        ReciprocalEndDisplacedThresholdElevation = rwy.ReciprocalEndDisplacedThresholdElevation,
                        ReciprocalEndTouchdownZoneElevation = rwy.ReciprocalEndTouchdownZoneElevation,
                    };
                    _ = await _database.InsertAsync(dbRwy);
                }
                else if (landingStructure.GetType() == typeof(FAADataParser.NASR.Apt.Helipad))
                {
                    var helipad = (FAADataParser.NASR.Apt.Helipad)landingStructure;
                    var dbHelipad = new Helipad
                    {
                        AirportPrimaryKey = dbAirport.FAAComputerIdentifier,
                        Identification = helipad.Identification,
                        Latitude = helipad.Latitude,
                        Longitude = helipad.Longitude,
                        Elevation = helipad.Elevation,
                    };
                    _ = await _database.InsertAsync(dbHelipad);
                }
                else if (landingStructure.GetType() == typeof(FAADataParser.NASR.Apt.BalloonPort))
                {
                    var balloonport = (FAADataParser.NASR.Apt.BalloonPort)landingStructure;
                    var dbBalloonPort = new BalloonPort
                    {
                        AirportPrimaryKey = dbAirport.FAAComputerIdentifier,
                        Identification = balloonport.Identification,
                        Latitude = balloonport.Latitude,
                        Longitude = balloonport.Longitude,
                        Elevation = balloonport.Elevation,
                    };
                    _ = await _database.InsertAsync(dbBalloonPort);
                }
                else
                {
                    throw new ApplicationException("Unknown landing structure type");
                }
            }
        }
        public Database() => InitializeAsync().SafeFireAndForget(false);
        async Task InitializeAsync()
        {
            if (!_database.TableMappings.Any(m => m.MappedType.Name == typeof(Airport).Name))
            {
                _ = await _database.CreateTablesAsync(CreateFlags.None, typeof(Airport)).ConfigureAwait(false);
            }
            if (!_database.TableMappings.Any(m => m.MappedType.Name == typeof(BalloonPort).Name))
            {
                _ = await _database.CreateTablesAsync(CreateFlags.None, typeof(BalloonPort)).ConfigureAwait(false);
            }
            if (!_database.TableMappings.Any(m => m.MappedType.Name == typeof(Helipad).Name))
            {
                _ = await _database.CreateTablesAsync(CreateFlags.None, typeof(Helipad)).ConfigureAwait(false);
            }
            if (!_database.TableMappings.Any(m => m.MappedType.Name == typeof(Runway).Name))
            {
                _ = await _database.CreateTablesAsync(CreateFlags.None, typeof(Runway)).ConfigureAwait(false);
            }
        }

        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(DatabasePath, Flags));
        private static readonly SQLiteAsyncConnection _database = lazyInitializer.Value;
        private static string DatabasePath
        {
            get
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }
        private const string DatabaseFileName = "Database.db3";
        private const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;
    }
}
