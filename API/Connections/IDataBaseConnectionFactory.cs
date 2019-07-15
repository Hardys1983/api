using System.Data;

namespace API.Connections
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetReadOnlyConnection();
        IDbConnection GetReadWriteConnection();
    }
}
