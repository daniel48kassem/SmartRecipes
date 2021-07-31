using System.Collections.Generic;

namespace FlashOrder
{
    public class RecipeParameters
    {
        public string Title { get; set; }
        public IList<string> Ingredients { get; set; }
    }
}