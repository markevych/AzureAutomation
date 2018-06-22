using DatabaseCreation.Models;

namespace DatabaseCreation.Interfaces
{
    interface IStatusService
    {
        void Add(StatusInfo status);
        void Update(string databaseName, string newStatus);
        StatusInfo Get(string databaseName);
    }
}
