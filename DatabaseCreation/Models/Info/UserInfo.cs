using DatabaseCreation.Services.Extensions;
using System;

namespace DatabaseCreation.Models
{
    public class UserInfo
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public static UserInfo Generate(string databaseName)
        {
            UserInfo generatedUser = new UserInfo
            {
                Login = databaseName + DateTime.Now.Year.ToString()
            };

            generatedUser.GeneratePassword();

            return generatedUser;
        }
    }
}