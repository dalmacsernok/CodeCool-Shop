using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class UserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public OrderAddress BillingAddress { get; set; }
        public OrderAddress ShippingAddress { get; set; }
    }
}
