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
    public class OrderDaoMemory : IOrderDao
    {
        private List<Order> data = new List<Order>();
        private static OrderDaoMemory instance = null;
        private readonly string _connectionString;
        private readonly string _mode;

        private OrderDaoMemory(string connectionString, string mode)
        {
            _connectionString = connectionString;
            _mode = mode;
        }

        public static OrderDaoMemory GetInstance(string connectionString, string mode)
        {
            if (instance == null)
            {
                instance = new OrderDaoMemory(connectionString, mode);
            }

            return instance;
        }

        public void Add(Order order)
        {
            order.Id = data.Count + 1;
            data.Add(order);
        }

        public void Add(Order order, bool alma)
        {
            const string cmdText = @"INSERT INTO order_data (name, email, phone_number, billing_address, shipping_address)
                                VALUES (@name, @email, @phone_number, @billing_address, @shipping_address);";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@name", order.CustomerData.Name);
                    cmd.Parameters.AddWithValue("@email", order.CustomerData.Email);
                    cmd.Parameters.AddWithValue("@phone_number", order.CustomerData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@billing_address", $"{order.CustomerData.BillingAddress.Country} {order.CustomerData.BillingAddress.City} {order.CustomerData.BillingAddress.ZipCode} {order.CustomerData.BillingAddress.Address}");
                    cmd.Parameters.AddWithValue("@shipping_address", $"{order.CustomerData.ShippingAddress.Country} {order.CustomerData.ShippingAddress.City} {order.CustomerData.ShippingAddress.ZipCode} {order.CustomerData.ShippingAddress.Address}");

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Remove(int id)
        {
            data.Remove(Get(id));
        }

        public Order Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return data;
        }
    }
}
