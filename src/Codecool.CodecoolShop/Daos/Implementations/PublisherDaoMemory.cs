using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class PublisherDaoMemory : IPublisherDao
    {
        private List<Publisher> data = new List<Publisher>();
        private static PublisherDaoMemory instance = null;
        private readonly string _connectionString;
        private readonly string _mode;

        private PublisherDaoMemory(string connectionString, string mode)
        {
            _connectionString = connectionString;
            _mode = mode;
        }

        public static PublisherDaoMemory GetInstance(string connectionString, string mode)
        {
            if (instance == null)
            {
                instance = new PublisherDaoMemory(connectionString, mode);
            }

            return instance;
        }

        public void Add(Publisher item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public Publisher Get(int id)
        {
            if (_mode == "memory")
            {
                return data.Find(x => x.Id == id);
            }

            const string cmdText = @"SELECT publisher_name
                            FROM publisher
                            WHERE id = @Id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        return null;
                    }
                    var name = reader.GetString("publisher_name");

                    var publisher = new Publisher() { Id = id, Name = name };
                    connection.Close();
                    return publisher;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Publisher> GetAll()
        {
            if (_mode == "memory")
            {
                return data.OrderBy(publisher => publisher.Name);
            }

            const string cmdText = @"SELECT id, publisher_name
                            FROM publisher";
            try
            {
                var results = new List<Publisher>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var name = reader["publisher_name"] as string;
                        int id = (int)reader["id"];

                        var publisher = new Publisher()
                        {
                            Id = id,
                            Name = name
                        };

                        results.Add(publisher);
                    }

                    connection.Close();
                }

                return results.OrderBy(result=>result.Name);
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}
