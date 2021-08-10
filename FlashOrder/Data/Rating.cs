using System.ComponentModel.DataAnnotations;

namespace FlashOrder.Data
{
    public class Rating
    {
        [Required]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public ApiUser User { get; set; }
        
        [Range(0,5)]
        [Required]
        public float Value { get; set; }
    }
}