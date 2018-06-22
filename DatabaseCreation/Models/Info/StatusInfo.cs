namespace DatabaseCreation.Models
{
    public class StatusInfo
    {
        public DatabaseInfo DbInfo { get; set; }
        public string CurrentStatus { get; set; }

        public StatusInfo(DatabaseInfo _databaseInfo, string _currentStatus)
        {
            DbInfo = _databaseInfo;
            CurrentStatus = _currentStatus;
        }
    }
}