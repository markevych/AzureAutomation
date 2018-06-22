using DatabaseCreation.Models;

namespace DatabaseCreation.Interfaces
{
    interface IDatabaseService
    {
        void Add(DatabaseInfo database);
        void Edit(DatabaseInfo database);
        void Create(string databaseName);
        void AddUser(string databaseName);
    }
}
