using System;

namespace DatabaseCreation.Helpers.Exceptions
{
    public class DatabaseCreationFailedException : Exception
    {
        public DatabaseCreationFailedException() 
        {

        }

        public DatabaseCreationFailedException(string name)
            : base(String.Format($"Database {name} wasn't created"))
        {

        }
    }
}