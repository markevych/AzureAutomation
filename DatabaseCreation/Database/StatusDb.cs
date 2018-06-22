using DatabaseCreation.Interfaces;
using DatabaseCreation.Models;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseCreation.Database
{
    public class StatusDb : IStatusDb
    {
        private static StatusDb instance;

        public List<StatusInfo> Statuses { get; } = new List<StatusInfo>();
        public List<DatabaseInfo> Databases { get; } = new List<DatabaseInfo>();
        public List<Log> Logs { get; } = new List<Log>();

        private StatusDb()
        { }

        public static StatusDb GetInstance()
        {
            if (instance == null)
                instance = new StatusDb();
            return instance;
        }

        public void AddStatus(StatusInfo status)
        {
            Statuses.Add(status);
        }

        public void UpdateStatus(string databaseName, string newStatus)
        {
            var statusInfo = Statuses.First(p => p.DbInfo.Name == databaseName);
            statusInfo.CurrentStatus = newStatus;
        }

        public StatusInfo GetStatus(string databaseName)
        {
            return Statuses.FirstOrDefault(p => p.DbInfo.Name == databaseName);
        }

        public void AddDatabase(DatabaseInfo database)
        {
            Databases.Add(database);
        }

        public void AddUserDb(string databaseName, UserInfo user)
        {
            var databaseInfo = Databases.First(p => p.Name == databaseName);
            databaseInfo.DefaultUser = user;
        }

        public void AddLog(Log log)
        {
            Logs.Add(log);
        }
    }
}