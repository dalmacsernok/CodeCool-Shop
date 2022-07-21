using System;
using Microsoft.Data.SqlClient;

namespace Codecool.BookDb.Manager
{
    public class BookDbManager
    {
        public string ConnectionString => System.Configuration.ConfigurationManager.AppSettings["ConnectionStrings"];
        public string Mode => System.Configuration.ConfigurationManager.AppSettings["Mode"];

        public bool TestConnection()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public void EnsureConnectionSuccessful()
        {
            if (!TestConnection())
            {
                Console.WriteLine("Connection failed, exit!");
                Environment.Exit(1);
            }
            Console.WriteLine("Connection successful!");
        }

        
    }
}