using Microsoft.AspNetCore.Http;

namespace FlashOrder.DTOs
{
    //this class just for hold the file from the user request
    public class UploadedFileDTO
    {
        public IFormFile MyFile { get; set; }
    }
}