using System.Data.SqlClient;

namespace DatabaseCreation.Services.Extensions
{
    public static class SqlCommandExtension
    {
        public static void RunQuery(this string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}