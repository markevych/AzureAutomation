using DatabaseCreation.Database;
using DatabaseCreation.Models;
using System.Configuration;
using DatabaseCreation.Services.Extensions;
using DatabaseCreation.Constants;
using System;
using System.Web.Script.Serialization;
using DatabaseCreation.Helpers.Exceptions;
using DatabaseCreation.Exceptions;
using DatabaseCreation.Interfaces;

namespace DatabaseCreation.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IStatusService statusService;
        private readonly IStatusDb db;

        private static DatabaseService instance;

        private DatabaseService()
        {
            statusService = StatusService.GetInstance();
            db = StatusDb.GetInstance();
        }

        public static DatabaseService GetInstance()
        {
            if (instance == null)
                instance = new DatabaseService();
            return instance;
        }

        public void Add(DatabaseInfo database)
        {
            db.AddDatabase(database);
            db.AddStatus(new StatusInfo(database, StatusValue.New));
        }

        public void Edit(DatabaseInfo database)
        {
            var editDatabase = db.GetStatus(database.Name);

            editDatabase.DbInfo.DbServerName = database.DbServerName;
            editDatabase.DbInfo.AzureResourceGroup = database.AzureResourceGroup;
            editDatabase.DbInfo.DbTierType = database.DbTierType;
        }
        
        public void Create(string databaseName)
        {
            var status = statusService.Get(databaseName);

            switch(status.CurrentStatus)
            {
                case StatusValue.CreationFailed:
                case StatusValue.New:
                    var uri = ConfigurationManager.AppSettings["WebhookUri"];
                    var data = new JavaScriptSerializer().Serialize(status.DbInfo);
                    statusService.Update(databaseName, StatusValue.InDeployment);

                    try
                    {
                        uri.Post(data);
                    }
                    catch
                    {
                        statusService.Update(databaseName, StatusValue.CreationFailed);
                        throw new DatabaseCreationFailedException(databaseName);
                    }
                    statusService.Update(databaseName, StatusValue.CreationComplete);

                    break;
                    
                case StatusValue.UserCreationFailed:
                    AddUser(databaseName);
                    break;

                default:
                    throw new DatabaseIsDeployedException(databaseName, status.CurrentStatus);
            }            
        }

        public void AddUser(string databaseName)
        {
            UserInfo newUser = UserInfo.Generate(databaseName);
            
            string connectionStringFormat = ConfigurationManager.AppSettings["ConnectionStringFormat"];
            string customConnectionString = string.Format(connectionStringFormat, databaseName);
            string masterConnectionString = string.Format(connectionStringFormat, "master");

            try
            {
                AddToMaster(newUser, masterConnectionString);
                AddToCustom(newUser, customConnectionString, databaseName);

                statusService.Update(databaseName, StatusValue.UserCreationComplete);
                db.AddUserDb(databaseName, newUser);
            }
            catch 
            {
                statusService.Update(databaseName, StatusValue.UserCreationFailed);
                throw new Exception("The problem is in user creation");
            }
        }

        private void AddToMaster(UserInfo newUser, string connFormat)
        {
            string queryString = $"CREATE LOGIN {newUser.Login} WITH PASSWORD = '{newUser.Password}'; ";
            string connectionString = string.Format(connFormat, "master");
            queryString.RunQuery(connectionString);
        }

        private void AddToCustom(UserInfo newUser, string connFormat, string databaseName)
        {
            string queryString = $"CREATE USER {newUser.Login} FROM LOGIN {newUser.Login}; ";
            string connectionString = string.Format(connFormat, databaseName);
            queryString.RunQuery(connectionString);
        }
    }
}
