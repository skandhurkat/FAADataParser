using System;
using System.IO;
using System.Threading.Tasks;

using SQLite;

namespace FAADatabase
{
    public class Database
    {
        public Database() => InitializeAsync().SafeFireAndForget(false);

        async Task InitializeAsync()
        {
            // Placeholder, just to shut up compiler warnings
            _ = await Task.FromResult(0);
        }

        private static readonly SQLiteAsyncConnection _database = lazyInitializer.Value;
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(DatabasePath, Flags));
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
