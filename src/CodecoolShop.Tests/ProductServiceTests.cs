using Castle.Core.Internal;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CodecoolShop.Tests
{
    [TestFixture]
    public class Tests
    {
        private IGenreDao genreMock;
        private IProductDao productMock;
        private IPublisherDao publisherMock;
        private IOrderDao orderMock;

        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            genreMock = Substitute.For<IGenreDao>();
            productMock = Substitute.For<IProductDao>();
            publisherMock = Substitute.For<IPublisherDao>();
            orderMock = Substitute.For<IOrderDao>();

            _productService = new ProductService(productMock, genreMock, publisherMock, orderMock);

        }

        [Test]
        public void GetProductCategoryReturnsValidGenre()
        {
            Genre expected = new Genre
            {
                Name = "Literature"
            };

            genreMock.Get(1).Returns(expected);

            Assert.AreEqual(_productService.GetProductCategory(1), expected);
        }

        [Test]
        public void GetProductCategoryNonExistingIndexThrowsNullReferenceException()
        {
            genreMock.Get(100).Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() => _productService.GetProductCategory(100));
        }

        [Test]
        public void GetProductsForCategoryReturnsValidProductsEnumerable()
        {
            IEnumerable<Product> expected = new[] { new Product() };

            Genre genre = new Genre
            {
                Name = "Literature"
            };

            genreMock.Get(1).Returns(genre);

            productMock.GetBy(genre).Returns(expected);

            Assert.AreEqual(_productService.GetProductsForCategory(1), expected);
        }

        [Test]
        public void GetProductsForCategoryNonExistingCategoryThrowsNullReferenceException()
        {
            genreMock.Get(100).Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() => _productService.GetProductsForCategory(100));
        }


        [Test]
        public void GetProductsForPublisherReturnsValidProductsEnumerable()
        {
            IEnumerable<Product> expected = new[] { new Product() };

            Publisher publisher = new Publisher()
            {
                Name = "Európa"
            };

            publisherMock.Get(1).Returns(publisher);

            productMock.GetBy(publisher).Returns(expected);

            Assert.AreEqual(_productService.GetProductsForPublisher(1), expected);
        }

        [Test]
        public void GetProductsForPublisherNonExistingPublisherThrowsNullReferenceException()
        {
            publisherMock.Get(100).Throws(new NullReferenceException());

            Assert.Throws<NullReferenceException>(() => _productService.GetProductsForPublisher(100));
        }

        [Test]
        public void DecreaseProductQuantityReturnsValidQuantityNumber()
        {
            Order order = new Order();

            Product product = new Product()
            {
                Author = "Frank Herbert",
                Name = "Dűne",
                DefaultPrice = 5299.0m,
                Currency = "HUF",
                Description =
                    "Az ​univerzum legfontosabb terméke a fűszer, amely meghosszabbítja az életet, lehetővé teszi az űrutazást, és élő számítógépet csinál az emberből. Az emberlakta világokat uraló Impériumban azé a hatalom, aki a fűszert birtokolja. A Padisah Császár, a bolygókat uraló Nagy Házak, az Űrliga és a titokzatos rend, a Bene Gesserit kényes hatalmi egyensúlyának, a civilizáció egészének záloga, hogy a fűszerből nem lehet hiány.",
                Genre = new Genre { Name = "Science Fiction" },
                Publisher = new Publisher { Name = "Kozmosz" },
                ReleaseDate = 1987
            };

            LineItem lineItem = new LineItem(product, 3);

            order.Cart.Add(lineItem);

            orderMock.Get(1).Returns(order);

            _productService.DecreaseProductQuantity(0);

            int expected = order.Cart[0].Quantity;

            Assert.AreEqual(expected, 2);
        }

        [Test]
        public void DecreaseProductQuantityNonExistingProductThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => _productService.DecreaseProductQuantity(0));
        }

        [Test]
        public void IncreaseProductQuantityReturnsValidQuantityNumber()
        {
            Order order = new Order();

            Product product = new Product()
            {
                Author = "Frank Herbert",
                Name = "Dűne",
                DefaultPrice = 5299.0m,
                Currency = "HUF",
                Description =
                    "Az ​univerzum legfontosabb terméke a fűszer, amely meghosszabbítja az életet, lehetővé teszi az űrutazást, és élő számítógépet csinál az emberből. Az emberlakta világokat uraló Impériumban azé a hatalom, aki a fűszert birtokolja. A Padisah Császár, a bolygókat uraló Nagy Házak, az Űrliga és a titokzatos rend, a Bene Gesserit kényes hatalmi egyensúlyának, a civilizáció egészének záloga, hogy a fűszerből nem lehet hiány.",
                Genre = new Genre { Name = "Science Fiction" },
                Publisher = new Publisher { Name = "Kozmosz" },
                ReleaseDate = 1987
            };

            LineItem lineItem = new LineItem(product, 3);

            order.Cart.Add(lineItem);

            orderMock.Get(1).Returns(order);

            _productService.IncreaseProductQuantity(0);

            int expected = order.Cart[0].Quantity;

            Assert.AreEqual(expected, 4);
        }

        [Test]
        public void IncreaseProductQuantityNonExistingProductThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => _productService.DecreaseProductQuantity(0));
        }

        [Test]
        public void DeleteItemRemoveCorrectElementReturnTrue()
        {
            Order order = new Order();

            Product product = new Product()
            {
                Author = "Frank Herbert",
                Name = "Dűne",
                DefaultPrice = 5299.0m,
                Currency = "HUF",
                Description =
                    "Az ​univerzum legfontosabb terméke a fűszer, amely meghosszabbítja az életet, lehetővé teszi az űrutazást, és élő számítógépet csinál az emberből. Az emberlakta világokat uraló Impériumban azé a hatalom, aki a fűszert birtokolja. A Padisah Császár, a bolygókat uraló Nagy Házak, az Űrliga és a titokzatos rend, a Bene Gesserit kényes hatalmi egyensúlyának, a civilizáció egészének záloga, hogy a fűszerből nem lehet hiány.",
                Genre = new Genre { Name = "Science Fiction" },
                Publisher = new Publisher { Name = "Kozmosz" },
                ReleaseDate = 1987
            };

            LineItem lineItem = new LineItem(product, 3);

            order.Cart.Add(lineItem);

            orderMock.Get(1).Returns(order);

            _productService.DeleteItem(0);

            Assert.IsTrue(order.Cart.IsNullOrEmpty());
        }

        [Test]
        public void DeleteItemNonExistingItemThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => _productService.DeleteItem(0));
        }

        [Test]
        public void GetOrderReturnsValidOrder()
        {
            Order expected = new Order();
            orderMock.Get(1).Returns(expected);
            Assert.AreEqual(_productService.GetOrder(1), expected);
        }

        [Test]
        public void GetOrderNonExistingOrderThrowsNullReferenceException()
        {
            orderMock.Get(100).Throws(new NullReferenceException());
            Assert.Throws<NullReferenceException>(() => _productService.GetOrder(100));
        }

    }
}