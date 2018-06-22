using System;

namespace DatabaseCreation.Exceptions
{
    public class DatabaseIsDeployedException : Exception
    {
        public DatabaseIsDeployedException()
        {

        }

        public DatabaseIsDeployedException(string name, string status)
            : base(String.Format($"Database {name} is already deployed and it's status is {status}"))
        {

        }
    }
}