using System.Collections.Generic;

namespace FlashOrder.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ChefId { get; set; }
        public string Description { get; set; }
        
        public virtual IList<Ingredient> Ingredients { get; set; }
    }
}