using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace FlashOrder.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Image Thumbnail { get; set; }
        
        // [ForeignKey(nameof(Ingredient))]
        // public int IngredientId { get; set; }
        // public Ingredient Ingredient { get; set; }

    }
}