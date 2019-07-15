using API.Configuration.Settings;
using API.Connections;
using API.Exceptions;
using Autofac;

namespace API.Configuration.Mapping
{
    public class Database: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var config = c.Resolve<ConfigurationSettings>();

                if (string.IsNullOrEmpty(config.ConnectionStrings.SqlReadOnlyConnection))
                {
                    throw new DatabaseConnectionConfigurationException("Readonly Connection string does not exist.");
                }

                if (string.IsNullOrEmpty(config.ConnectionStrings.SqlReadWriteConnection))
                {
                    throw new DatabaseConnectionConfigurationException("Read write Connection string does not exist.");
                }

                return new DatabaseConnectionFactory(
                    config.ConnectionStrings.SqlReadOnlyConnection,
                    config.ConnectionStrings.SqlReadWriteConnection);

            }).As<IDatabaseConnectionFactory>()
                .InstancePerLifetimeScope();
        }
    }
}
