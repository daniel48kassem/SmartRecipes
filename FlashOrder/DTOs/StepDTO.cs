using System.ComponentModel.DataAnnotations.Schema;
using FlashOrder.Data;

namespace FlashOrder.DTOs
{
    public class CreateStepDTO
    {
        public int Order { get; set; }
        public string Description { get; set; }
    }
    
    public class StepDTO:CreateStepDTO
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
        // public Recipe Recipe { get; set; }
    }
}