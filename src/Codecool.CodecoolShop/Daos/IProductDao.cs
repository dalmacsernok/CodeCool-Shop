using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDao : IDao<Product>
    {
        IEnumerable<Product> GetBy(Publisher publisher);
        IEnumerable<Product> GetBy(Genre genre);
    }
}
