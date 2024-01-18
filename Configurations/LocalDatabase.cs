using Microsoft.Extensions.Options;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Configurations
{
    public class LocalDatabase : ILocalDatabase
    {
        private readonly ConnectionStrings _connectionString;

        public LocalDatabase(IOptions<ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value;
        }

    public string GetLocalDbPath()
    {
        return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), _connectionString.LocalDbPath);
    }

    public SQLiteConnection SQLiteConnection()
        {
            return new SQLiteConnection(GetLocalDbPath());
        }

       
    }
}
