using System.ComponentModel.DataAnnotations.Schema;

namespace FlashOrder.Data
{
    public class Step
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        
        
        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}