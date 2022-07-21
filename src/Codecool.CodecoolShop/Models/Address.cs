using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class OrderAddress
    {
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }

    }
}
