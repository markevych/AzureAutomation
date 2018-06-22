using DatabaseCreation.Database;
using DatabaseCreation.Interfaces;
using DatabaseCreation.Models;

namespace DatabaseCreation.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusDb Db;
        private static StatusService instance;
        
        private StatusService()
        {
            Db = StatusDb.GetInstance();
        }

        public static StatusService GetInstance()
        {
            if (instance == null)
                instance = new StatusService();
            return instance;
        }

        public void Add(StatusInfo status)
        {
            Db.AddStatus(status);
            Db.AddLog(new Log(status.DbInfo.Name, status.CurrentStatus));
        }

        public void Update(string databaseName, string newStatus)
        {
            Db.UpdateStatus(databaseName, newStatus);
            Db.AddLog(new Log(databaseName, newStatus));
        }

        public StatusInfo Get(string databaseName)
        {
            return Db.GetStatus(databaseName);
        }
    }
}