using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using FlashOrder.Middlewares;

namespace FlashOrder.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public string ImagePath { get; set; }
        

        // public Image Image { get; set; }
        
        // [ForeignKey(nameof(Ingredient))]
        // public int IngredientId { get; set; }
        // public Ingredient Ingredient { get; set; }

    }
}