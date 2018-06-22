using DatabaseCreation.Models;

namespace DatabaseCreation.Interfaces
{
    interface IStatusDb
    {
        void AddStatus(StatusInfo status);
        void UpdateStatus(string databaseName, string newStatus);
        StatusInfo GetStatus(string databaseName);
        void AddDatabase(DatabaseInfo database);
        void AddUserDb(string databaseName, UserInfo user);
        void AddLog(Log log);
    }
}
