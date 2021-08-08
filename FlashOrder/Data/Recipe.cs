using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashOrder.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        [ ForeignKey("ApiUser")]
        public string ChefId { get; set; }
        public ApiUser Chef { get; set; }
        
        public string Description { get; set; }
        
        
        public virtual IList<Ingredient> Ingredients { get; set; }
        public virtual IList<Step> Steps { get; set; }

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