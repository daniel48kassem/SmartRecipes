using System.ComponentModel.DataAnnotations.Schema;

namespace FlashOrder.Data
{
    public class Ingredient
    {
        public int Id { get; set; }
        
        public Item Item { get; set; }
        
        public double Price { get; set; }
        
        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}