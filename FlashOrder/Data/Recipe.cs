namespace FlashOrder.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public int ChefId { get; set; }
        public string Description { get; set; }
        
        public Ingredient Ingredients { get; set; }
    }
}