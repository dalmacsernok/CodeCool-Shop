using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Codecool.CodecoolShop.Models
{
    public class Order : BaseModel
    {
        public List<LineItem> Cart { get; set; }

        public UserData CustomerData { get; set; }

        public Order()
        {
            Cart = new List<LineItem>();
        }

        public LineItem Get(int id)
        {
           return Cart.Find(item => item.Id == id);
        }

        public void AddItems(Product item)
        {
            foreach (var lineItem in Cart)
            {
                if (lineItem.Product.Name == item.Name)
                {
                    lineItem.Quantity++;
                    return;
                }
            }

            LineItem newItem = new LineItem(item, 1);
            newItem.Id = Cart.Count + 1;
            Cart.Add(newItem);
        }


        public int GetAllItemsNumber() => Cart.Sum(item => item.Quantity);

        public decimal GetTotalPrice() => Cart.Sum(item => item.Quantity * item.Product.DefaultPrice);

        public Object CloneOrder()
        {
            return this.MemberwiseClone();
        }
    }
}
