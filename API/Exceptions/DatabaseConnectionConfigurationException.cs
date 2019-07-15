using System;

namespace API.Exceptions
{
    public class DatabaseConnectionConfigurationException: Exception {
        public DatabaseConnectionConfigurationException(string message):
            base(message)
        {

        }
    }
}
