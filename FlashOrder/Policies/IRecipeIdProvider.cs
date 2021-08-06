using FlashOrder.Data;

namespace FlashOrder.Policies
{
    public interface IRecipeIdProvider
    {
        public string Get(Recipe recipe);
    }
}