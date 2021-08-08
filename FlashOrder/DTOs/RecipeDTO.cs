using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlashOrder.Data;

namespace FlashOrder.DTOs
{
    
    public class GeneralRecipeDTO
    {
        public string ChefId { get; set; }
        public UserDTO Chef { get; set; }
    }
    public class CreateRecipeDTO
    {
        [Required]        
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public virtual IList<CreateIngredientDTO> Ingredients { get; set; }
        [Required] 
        public virtual IList<CreateStepDTO> Steps { get; set; }
    }

    public class RecipeDTO : GeneralRecipeDTO
    {
            public int Id { get; set; }
            public IList<IngredientDTO> Ingredients { get; set; }
            public IList<StepDTO> Steps { get; set; }
            public double Cost { get; set; }
    }

    public class UpdateRecipeDTO : CreateRecipeDTO
    {
        
    }
}