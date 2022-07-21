using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;



namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IGenreDao _genreDao;
        private readonly IPublisherDao _publisherDao;
        private readonly IOrderDao orderDao;

        public ProductService(IProductDao productDao, IGenreDao genreDao, IPublisherDao publisherDao, IOrderDao orderDao)
        {
            this.productDao = productDao;
            this._genreDao = genreDao;
            this._publisherDao = publisherDao;
            this.orderDao = orderDao;
        }

        public Genre GetProductCategory(int categoryId)
        {
            return this._genreDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId = 1)
        {
            Genre category = this._genreDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<Product> GetProductsForPublisher(int supplierId = 1)
        {
            Publisher publisher = this._publisherDao.Get(supplierId);
            return this.productDao.GetBy(publisher);
        }

        public void DecreaseProductQuantity(int id)
        {
            var order = orderDao.Get(1);
            var lineItem = order.Get(id);
            lineItem.Quantity = lineItem.Quantity > 1 ? lineItem.Quantity - 1 : lineItem.Quantity;
        }

        public void IncreaseProductQuantity(int id)
        {
            var order = orderDao.Get(1);
            var lineItem = order.Get(id);
            lineItem.Quantity++;
        }
        public void DeleteItem(int id)
        {
            var order = orderDao.Get(1);
            var lineItem = order.Get(id);
            if (lineItem is null)
            {
                throw new NullReferenceException();
            }
            order.Cart.Remove(lineItem);
        }

        public Order GetOrder(int id)
        {
            var order = orderDao.Get(id);
            if (order is null)
                throw new NullReferenceException();
            return order;
        }
    }
}
