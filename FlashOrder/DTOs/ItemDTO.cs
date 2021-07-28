using System.ComponentModel.DataAnnotations.Schema;
using FlashOrder.Data;

namespace FlashOrder.DTOs
{
    public class CreateItemDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public int ImageId { get; set; }
        
        // public int IngredientId { get; set; }
    }

    public class ItemDTO : CreateItemDTO
    {       
        public int Id { get; set; }
        // public IngredientDTO Ingredient { get; set; }
        public ImageDTO Thumbnail { get; set; }
    }
}