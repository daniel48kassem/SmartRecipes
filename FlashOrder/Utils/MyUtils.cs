using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Utils
{
    public class MyUtils
    {
        private readonly ILogger<MyUtils> _logger;
        
        public MyUtils(ILogger<MyUtils> logger)
        {
            _logger = logger;
        }
   
        public async Task<string> SaveFileToPublicFolder(IFormFile file,string folderPath)
        {
            var folderName = Path.Combine("wwwroot", folderPath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            
            var returnPath = Path.Combine(folderName, fileName).Remove(0,8);;
            
            var fullPath = Path.Combine(pathToSave, fileName);
            
            try
            {
                await  using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }            
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(SaveFileToPublicFolder)}");
            }
        
            return returnPath;
        }
    }
}