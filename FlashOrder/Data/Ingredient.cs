namespace FlashOrder.Data
{
    public class Ingredient
    {
        public int Id { get; set; }
        
        public Item Item { get; set; }
        
        public double Price { get; set; }
        
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}