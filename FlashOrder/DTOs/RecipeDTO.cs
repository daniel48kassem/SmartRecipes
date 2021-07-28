using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlashOrder.Data;

namespace FlashOrder.DTOs
{
    
    public class GeneralRecipeDTO
    {
        [Required]        
        public string Title { get; set; }

        [Required] 
        public int ChefId { get; set; }
        
        [Required] 
        public string Description { get; set; }
    }
    public class CreateRecipeDTO:GeneralRecipeDTO
    {
        [Required] 
        public virtual IList<CreateIngredientDTO> Ingredients { get; set; }
    }

    public class RecipeDTO : GeneralRecipeDTO
    {
            public int Id { get; set; }
            public IList<IngredientDTO> Ingredients { get; set; }
    }
}