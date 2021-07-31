using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemController> _logger;
        
        public ItemController(ILogger<ItemController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
       
        private async Task<string> SaveFile(IFormFile file)
        {
            var folderName = Path.Combine("wwwroot", "images");
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
                _logger.LogError(e, $"something went wrong in {nameof(SaveFile)}");
            }
        
            return returnPath;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromForm] CreateItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"invalid post attempt{nameof(CreateItem)}");
                return BadRequest(ModelState);
            }

            try
            {
                var file = itemDTO.ImageFile;
                var path=await  SaveFile(file);
                
                var item = _mapper.Map<Item>(itemDTO);
                item.ImagePath = path;
                
                await _unitOfWork.Items.Insert(item);
                await _unitOfWork.save();
                return Ok();
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(CreateItem)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}", Name = "GetItem")]
        public async Task<IActionResult> GetItem(int id)
        {
            if (id<1)
            {
                _logger.LogError($"invalid post attempt{nameof(GetItem)}");
                return NotFound("Your Recipe cannot be found");
            }
            
            try
            {
                var item=await _unitOfWork.Items.Get(q=>q.Id==id);
                var res=_mapper.Map<GetItemDTO>(item);
                return Ok(res);
            }
        
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(GetItem)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id:int}", Name = "UpdateItem")]
        public async Task<IActionResult> UpdateItem(int id,[FromForm] UpdateItemDTO itemDTO)
        {
            if (id<1||!ModelState.IsValid)
            {
                _logger.LogError($"invalid post attempt{nameof(GetItem)}");
                return BadRequest("your data is invalid");
            }
            
            try
            {
                var item=await _unitOfWork.Items.Get(q=>q.Id==id);

                if (item == null)
                {
                    _logger.LogError($"invalid Update attempt in {nameof(UpdateItem)}");
                    return BadRequest("Submitted Data s not valid");
                }
                
                //updating image data
                var file = itemDTO.ImageFile;
                var path=await  SaveFile(file);
                
                item.ImagePath = path;

                //(source,out object)
                _mapper.Map(itemDTO,item);
                _unitOfWork.Items.Update(item);
                await _unitOfWork.save();
                
                return NoContent();
            }
        
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(UpdateItem)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // [HttpDelete("{id:int}", Name = "DeleteItem")]
        // public async Task<IActionResult> DeleteItem(int id)
        // {
        //     
        // }
    }
}