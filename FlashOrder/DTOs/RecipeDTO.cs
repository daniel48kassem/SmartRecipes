using System.ComponentModel.DataAnnotations;
using FlashOrder.Data;

namespace FlashOrder.DTOs
{
    public class RecipeDTO
    {
        [Required] 
        public int ChefId { get; set; }
        
        [Required] 
        public string Description { get; set; }
        
        [Required] 
        public Ingredient Ingredients { get; set; }
    }
}