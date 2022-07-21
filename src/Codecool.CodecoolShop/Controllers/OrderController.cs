using System.Collections.Generic;
using System.Diagnostics;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using Codecool.BookDb.Manager;
using EmailService;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly Serilog.ILogger _logger;
        private Random _random = new Random();
        private string _connectionString = new BookDbManager().ConnectionString;
        private string _mode = new BookDbManager().Mode;
        public ProductService ProductService { get; set; }

        public OrderController(IEmailSender emailSender)
        {
            _emailSender = emailSender;

            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(_connectionString, _mode),
                GenreDaoMemory.GetInstance(_connectionString, _mode),
                PublisherDaoMemory.GetInstance(_connectionString, _mode),
                OrderDaoMemory.GetInstance(_connectionString, _mode));
            
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Exceptions/Order/Log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void AddToCart(string data, string filter = "Category", int id = 1)
        {
            try
            {
                Product product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(data);
                Order order = OrderDaoMemory.GetInstance(_connectionString, _mode).Get(1);
                order.AddItems(product);

            }
            catch (Exception e)
            {
                _logger.Error(e, "Something went wrong :S");
                throw;
            }
            


            Response.Redirect($"/Product/Index/{filter}/{id}");
        }

        public IActionResult Cart()
        {
            Order order;
            try
            {
                order = OrderDaoMemory.GetInstance(_connectionString, _mode).Get(1);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Something went wrong :'(");
                throw;
            }
           

            ViewData["cartContent"] = order.GetAllItemsNumber();
            ViewData["categories"] = GenreDaoMemory.GetInstance(_connectionString, _mode).GetAll();
            ViewData["suppliers"] = PublisherDaoMemory.GetInstance(_connectionString, _mode).GetAll();

            ViewData["order"] = order;
            ViewData["totalPrice"] = order.GetTotalPrice();
            
            return View();
        }
        public void ChangeQuantity(string filter, int id)
        {
            try
            {
                switch (filter)
                {
                    case "minus":
                        ProductService.DecreaseProductQuantity(id);
                        break;
                    case "plus":
                        ProductService.IncreaseProductQuantity(id);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Something went wrong :'(");
                throw;
            }
           
            Response.Redirect($"/Order/Cart");
        }

        public void DeleteItem(int deleteid)
        {
            try
            {
                ProductService.DeleteItem(deleteid);
            }
            catch (Exception e)
            {
                _logger.Error(e, "SWW");
                throw;
            }
           
            Response.Redirect($"/Order/Cart");
        }

        public IActionResult CheckOut(int orderId)
        {
            Order order;
            
            try
            {
                order = ProductService.GetOrder(orderId);
            }
            catch (Exception e)
            {
                _logger.Error(e, "nemjó");
                throw;
            }
            
            ViewData["cartContent"] = order.GetAllItemsNumber();
            ViewData["categories"] = GenreDaoMemory.GetInstance(_connectionString, _mode).GetAll();
            ViewData["suppliers"] = PublisherDaoMemory.GetInstance(_connectionString, _mode).GetAll();

            ViewData["orderId"] = orderId;

            return View();
        }

        public IActionResult StoreUserData(UserData userData, int orderId)
        {
            Order order = ProductService.GetOrder(orderId);
            int randomNum = _random.Next(1,100);
            try
            {

                order.CustomerData = userData;
                if (randomNum == 1)
                    throw new IOException();
            }
            catch (Exception e)
            {
                _logger.Error(e, "shame");
                throw;
            }
            
            
            return RedirectToAction("Payment", new { orderId = orderId });
        }


        public IActionResult Payment(int orderId)
        {
            Order order;
            try
            { 
                order = ProductService.GetOrder(orderId);
            }
            catch (Exception e)
            {
                _logger.Error(e, "no pay");
                throw;
            }

            OrderDaoMemory.GetInstance(_connectionString, _mode).Add(order, true);


            ViewData["cartContent"] = order.GetAllItemsNumber();
            ViewData["categories"] = GenreDaoMemory.GetInstance(_connectionString, _mode).GetAll();
            ViewData["suppliers"] = PublisherDaoMemory.GetInstance(_connectionString, _mode).GetAll();

            ViewData["totalPrice"] = order.GetTotalPrice();
            ViewData["OrderId"] = orderId;

            return View();
        }


        public IActionResult OrderConfirmation(Payment payment, int orderId)
        {
            var order = ProductService.GetOrder(orderId);

            var message = new Message(new string[] { order.CustomerData.Email }, "Order Confirmation", $"Dear {order.CustomerData.Name}! Successful order!");
            _emailSender.SendEmail(message);

            ViewData["cartContent"] = order.GetAllItemsNumber();
            ViewData["categories"] = GenreDaoMemory.GetInstance(_connectionString, _mode).GetAll();
            ViewData["suppliers"] = PublisherDaoMemory.GetInstance(_connectionString, _mode).GetAll();

            var result = JsonConvert.SerializeObject(order);

            System.IO.File.WriteAllText($"./OrderLog/order_{order.Id}.json", result);

            ViewData["order"] = (Order)order.CloneOrder(); 

            
            order.Cart = new List<LineItem>();

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
