using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.BookDb.Manager;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Serilog;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly Serilog.ILogger _logger;

        private string _connectionString = new BookDbManager().ConnectionString;
        private string _mode = new BookDbManager().Mode;
        public ProductService ProductService { get; set; }

        public ProductController()
        {
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(_connectionString, _mode),
                GenreDaoMemory.GetInstance(_connectionString, _mode),
                PublisherDaoMemory.GetInstance(_connectionString, _mode),
                OrderDaoMemory.GetInstance(_connectionString, _mode));
           _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Exceptions/Product/Log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        
        public IActionResult Index(string filter = "Category", int id = 1)
        {
            BookDbManager bookDbManager = new BookDbManager();
            bookDbManager.EnsureConnectionSuccessful();
            List<Product> products = new();
            try
            {
                switch (filter)
                {
                    case "Category":
                        products = ProductService.GetProductsForCategory(id).ToList();
                        break;
                    case "Publisher":
                        products = ProductService.GetProductsForPublisher(id).ToList();
                        break;
                }
            }
            catch (Exception e)
            {
               _logger.Error(e, "Something went wrong :(");
                throw;
            }
           
            
            ViewData["cartContent"] = OrderDaoMemory.GetInstance(_connectionString, _mode).Get(1).GetAllItemsNumber();
            ViewData["categories"] = GenreDaoMemory.GetInstance(_connectionString, _mode).GetAll();
            ViewData["suppliers"] = PublisherDaoMemory.GetInstance(_connectionString, _mode).GetAll();

            ViewData["filter"] = filter;
            ViewData["id"] = id;

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
