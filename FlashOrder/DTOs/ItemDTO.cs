using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using FlashOrder.Data;
using FlashOrder.Middlewares;
using Microsoft.AspNetCore.Http;

namespace FlashOrder.DTOs
{
    public class GeneralItemDTO
    {     
        public string Name { get; set; }
        public double Price { get; set; }
    }
    public class CreateItemDTO:GeneralItemDTO
    {
        public IFormFile ImageFile { get; set; }
        // public int ImageId { get; set; }
        // public CreateImageDTO Image { get; set; }

        // public int IngredientId { get; set; }
    }

    public class ItemDTO : GeneralItemDTO
    {       
        public int Id { get; set; }
        public string ImagePath { get; set; }
        
        // public IngredientDTO Ingredient { get; set; }
        // public int ImageId { get; set; }
        // public ImageDTO Image { get; set; }
    }
    
    public class GetItemDTO:ItemDTO
    {
        public new string ImagePath => Path.Combine(MyHttpContext.AppBaseUrl ,base.ImagePath);
    }
}