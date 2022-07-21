using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoMemory : IProductDao
    {
        private List<Product> data = new List<Product>();
        private static ProductDaoMemory instance = null;
        private readonly string _connectionString;
        private readonly string _mode;

        private ProductDaoMemory(string connectionString, string mode)
        {
            _connectionString = connectionString;
            _mode = mode;
        }

        public static ProductDaoMemory GetInstance(string connectionString, string mode)
        {
            if (instance == null)
            {
                instance = new ProductDaoMemory(connectionString, mode);
            }

            return instance;
        }

        public void Add(Product item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            if (_mode == "memory")
            {
                return data.Find(x => x.Id == id);
            }

            const string cmdText = @"SELECT *
                            FROM book
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

                    var title = reader.GetString("title");
                    var description = reader.GetString("book_description");
                    var author = reader.GetString("author");
                    var genre = GenreDaoMemory.GetInstance(_connectionString, _mode)
                        .Get(reader.GetInt32("genre_id"));
                    var publisher = PublisherDaoMemory.GetInstance(_connectionString, _mode)
                        .Get(reader.GetInt32("publisher_id"));
                    var defaultPrice = reader.GetDecimal("default_price");
                    var relaseDate = reader.GetInt32("release_date");

                    var book = new Product() { Name = title, Description = description, Author = author, Genre = genre, Publisher = publisher, DefaultPrice = defaultPrice, ReleaseDate = relaseDate};
                    connection.Close();
                    return book;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            if (_mode == "memory")
            {
                return data;
            }

            const string cmdText = @"SELECT *
                            FROM book";
            try
            {
                var results = new List<Product>();
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
                        var title = reader.GetString("title");
                        var description = reader.GetString("book_description");
                        var author = reader.GetString("author");
                        var genre = GenreDaoMemory.GetInstance(_connectionString, _mode)
                            .Get(reader.GetInt32("genre_id"));
                        var publisher = PublisherDaoMemory.GetInstance(_connectionString, _mode)
                            .Get(reader.GetInt32("publisher_id"));
                        var defaultPrice = reader.GetDecimal("default_price");
                        var relaseDate = reader.GetInt32("release_date");

                        var book = new Product() { Name = title, Description = description, Author = author, Genre = genre, Publisher = publisher, DefaultPrice = defaultPrice, ReleaseDate = relaseDate };

                        results.Add(book);
                    }

                    connection.Close();
                }

                return results;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Product> GetBy(Publisher publisher)
        {
            if (_mode == "memory")
            {
                return data.Where(x => x.Publisher.Id == publisher.Id);
            }

            const string cmdText = @"SELECT *
                            FROM book
                            WHERE publisher_id = @publisherId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@publisherId", publisher.Id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var title = reader.GetString("title");
                        var description = reader.GetString("book_description");
                        var author = reader.GetString("author");
                        var genre = GenreDaoMemory.GetInstance(_connectionString, _mode)
                            .Get(reader.GetInt32("genre_id"));
                        var publusher = PublisherDaoMemory.GetInstance(_connectionString, _mode)
                            .Get(reader.GetInt32("publisher_id"));
                        var defaultPrice = reader.GetDecimal("default_price");
                        var relaseDate = reader.GetInt16("release_date");

                        var book = new Product() { Name = title, Description = description, Author = author, Genre = genre, Publisher = publusher, DefaultPrice = defaultPrice, ReleaseDate = relaseDate };

                        results.Add(book);
                    }

                    connection.Close();
                }

                return results;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Product> GetBy(Genre genre)
        {
            if (_mode == "memory")
            {
                return data.Where(x => x.Genre.Id == genre.Id);
            }

            const string cmdText = @"SELECT *
                            FROM book
                            WHERE genre_id = @genreId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@genreId", genre.Id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var title = reader.GetString("title");
                        var description = reader.GetString("book_description");
                        var author = reader.GetString("author");
                        var zsanrák = GenreDaoMemory.GetInstance(_connectionString, _mode)
                            .Get(reader.GetInt32("genre_id"));
                        var publisher = PublisherDaoMemory.GetInstance(_connectionString, _mode)
                            .Get(reader.GetInt32("publisher_id"));
                        var defaultPrice = reader.GetDecimal("default_price");
                        var relaseDate = reader.GetInt16("release_date");

                        var book = new Product() { Name = title, Description = description, Author = author, Genre = zsanrák, Publisher = publisher, DefaultPrice = defaultPrice, ReleaseDate = relaseDate };

                        results.Add(book);
                    }

                    connection.Close();
                }

                return results;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}
