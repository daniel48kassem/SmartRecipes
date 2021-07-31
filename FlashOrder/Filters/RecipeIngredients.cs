using System;
using System.Collections.Generic;
using System.Linq;
using FlashOrder.Data;

namespace FlashOrder
{
    public class RecipeContainsIngredients : BaseSpecification<Recipe>
    {
        public RecipeContainsIngredients(IList<string> queredIngredients)
        {
            Console.WriteLine(queredIngredients[0]);
            
            Criteria = r => r.Ingredients
                .Any(ing => queredIngredients.Any(queredIngredient=>ing.Item.Name.Contains(queredIngredient)));
            
            // ctx.posts.Where(post => words.Any(word => post.Title.Contains(word)))
            // Criteria = r => r.Ingredients.Where(i=>i.Id==4).Any();
        }
    }
}