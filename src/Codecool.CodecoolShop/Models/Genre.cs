using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Genre: BaseModel
    {
        public List<Product> Products { get; set; }
        //public string Department { get; set; }
    }
}
