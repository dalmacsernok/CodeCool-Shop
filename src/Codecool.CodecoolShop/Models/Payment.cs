using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class Payment
    {
        public int CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string ExpiryDate { get; set; }
        public int CVVCode { get; set; }
    }
}
