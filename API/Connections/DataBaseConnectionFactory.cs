using System.Data;
using System.Data.SqlClient;

namespace API.Connections
{
    public class DatabaseConnectionFactory: IDatabaseConnectionFactory
    {
        private static readonly object @lock = new object();

        private readonly string _readOnlyConnectionString;
        private readonly string _readWriteConnectionString;

        public DatabaseConnectionFactory(string readOnlyConnectionString, string readWriteConnectionString)
        {
            _readOnlyConnectionString = readOnlyConnectionString;
            _readWriteConnectionString = readWriteConnectionString;
        }

        public IDbConnection GetReadOnlyConnection()
        {
            lock (@lock)
            {
                return new SqlConnection(_readOnlyConnectionString);
            }
        }

        public IDbConnection GetReadWriteConnection()
        {
            lock (@lock)
            {
                return new SqlConnection(_readWriteConnectionString);
            }
        } 
    }
}
