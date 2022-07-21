namespace Codecool.CodecoolShop.Models
{
    public class LineItem : BaseModel
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public LineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
