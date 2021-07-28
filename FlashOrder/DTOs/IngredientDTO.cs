using System.ComponentModel.DataAnnotations;

namespace FlashOrder.DTOs
{
    public class CreateIngredientDTO
    {
        [Required] public double Qty { get; set; }
        [Required] public int ItemId { get; set; }
    }

    public class IngredientDTO : CreateIngredientDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public RecipeDTO Recipe { get; set; }
        public ItemDTO Item { get; set; }
    }
}