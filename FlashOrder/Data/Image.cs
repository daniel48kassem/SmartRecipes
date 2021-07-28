using Microsoft.AspNetCore.Http;

namespace FlashOrder.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public string path { get; set; }
        
        
        // public IFormFile ImageData { get; set; }
    }
    
    
}