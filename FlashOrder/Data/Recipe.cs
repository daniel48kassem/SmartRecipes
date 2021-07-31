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

        public double CalculateCost()
        {
            var cost = 0.0;
            foreach (var ingredient in this.Ingredients)
            {
                cost += ingredient.Cost();
            }
            return cost;
        }
        
    }
}