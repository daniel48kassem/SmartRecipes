using Microsoft.AspNetCore.Http;

namespace FlashOrder.DTOs
{
    public class UploadedFileDTO
    {
        public IFormFile MyFile { get; set; }
    }
}