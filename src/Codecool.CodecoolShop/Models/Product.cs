using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class Product : BaseModel
    {
        public string Currency { get; set; }
        public decimal DefaultPrice { get; set; }
        public Genre Genre { get; set; }
        public Publisher Publisher { get; set; }
        public int ReleaseDate { get; set; }
        public string Author { get; set; }

        public void SetProductCategory(Genre genre)
        {
            Genre = genre;
            Genre.Products.Add(this);
        }
    }
}
