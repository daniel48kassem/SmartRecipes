using System.ComponentModel.DataAnnotations;

namespace FlashOrder.DTOs
{
    public class RateDTO
    {
        [Required]
        public int RecipeId { get; set; }
        
        [Range(0,5)]
        [Required]
        public float Value { get; set; }
    }
}