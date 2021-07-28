using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlashOrder.Data;

namespace FlashOrder.DTOs
{
    public class CreateRecipeDTO
    {
        [Required] 
        public int ChefId { get; set; }
        
        [Required] 
        public string Description { get; set; }
        
        [Required] 
        public IList<CreateIngredientDTO> Ingredients { get; set; }
    }

    public class RecipeDTO : CreateRecipeDTO
    {
            public int Id { get; set; }
            public IList<IngredientDTO> Ingredients { get; set; }
    }
}