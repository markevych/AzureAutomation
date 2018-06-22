namespace DatabaseCreation.Models
{
    public class DatabaseInfo
    {
        public string Name { get; set; }
        public string AzureResourceGroup { get; set; }
        public string DbServerName { get; set; }
        public string DbTierType { get; set; }
        public UserInfo DefaultUser { get; set; }
    }
}