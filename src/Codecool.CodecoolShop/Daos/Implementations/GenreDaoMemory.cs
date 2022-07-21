using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class GenreDaoMemory : IGenreDao
    {
        private List<Genre> data = new List<Genre>();
        private static GenreDaoMemory instance = null;
        private readonly string _connectionString;
        private readonly string _mode;

        private GenreDaoMemory(string connectionString, string mode)
        {
            _connectionString = connectionString;
            _mode = mode;
        }

        public static GenreDaoMemory GetInstance(string connectionString, string mode)
        {
            if (instance == null)
            {
                instance = new GenreDaoMemory(connectionString, mode);
            }

            return instance;
        }

        public void Add(Genre item)
        {
            item.Id = data.Count + 1;
                data.Add(item);
        }

        public Genre Get(int id)
        {

            if (_mode == "memory")
            {
                return data.Find(x => x.Id == id);
            }
            
            const string cmdText = @"SELECT genre_name
                            FROM genre
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
                    var name = reader.GetString("genre_name");
                    
                    var genre = new Genre() { Id = id, Name = name };
                    connection.Close();
                    return genre;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public IEnumerable<Genre> GetAll()
        {
            if (_mode == "memory")
            {
                return data.OrderBy(genre => genre.Name);
            }

            const string cmdText = @"SELECT id, genre_name
                            FROM genre";
            try
            {
                var results = new List<Genre>();
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
                        var name = reader["genre_name"] as string;
                        int id = (int) reader["id"];

                        var genre = new Genre()
                        {
                            Id = id,
                            Name = name
                        };

                        results.Add(genre);
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
