using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FlashOrder.DTOs
{
    public class CreateImageDTO
    {
        [Required]
        public string ImageTitle { get; set; }
        // [Required]
        // public IFormFile ImageData { get; set; }

    }

    public class ImageDTO : CreateImageDTO
    {
        public int Id { get; set; }
        public string path { get; set; }

    }
}