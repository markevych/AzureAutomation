using DatabaseCreation.Models;
using System;
using System.Text;

namespace DatabaseCreation.Services.Extensions
{
    public static class SecurityManagementExtension
    {
        public static void GeneratePassword(this UserInfo user)
        {
            int length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            user.Password = res.ToString();
        }
    }
}