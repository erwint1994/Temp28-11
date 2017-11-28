using System;
using System.Configuration;
using System.Data.SqlClient;


namespace paSSQL
{
    public static class SQL
    {
        private static SqlConnection connection;
        private static string MyConnectionString;

        static SQL()
        {
            MyConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            connection = new SqlConnection(MyConnectionString);
        }

    }
}
