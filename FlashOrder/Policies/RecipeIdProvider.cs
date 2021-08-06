using FlashOrder.Data;

namespace FlashOrder.Policies
{
    public class ChefIdProvider
    {
        
        public string Get(Recipe recipe)
        {
            return recipe.ChefId;
        }
    }
}