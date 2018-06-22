using System;

namespace DatabaseCreation.Models
{
    public class Log
    {
        public string DatabaseName { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime TimeCreation { get; set; }

        public Log(string _databaseName, string _currentStatus)
        {
            DatabaseName = _databaseName;
            CurrentStatus = _currentStatus;
            TimeCreation = DateTime.Now;
        }
    }
}