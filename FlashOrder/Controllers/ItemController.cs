using System;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using FlashOrder.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemController> _logger;
        private readonly MyUtils _myUtils;
        
        public ItemController(ILogger<ItemController> logger, IMapper mapper, IUnitOfWork unitOfWork,MyUtils myUtils)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _myUtils = myUtils;
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
                var path=await  _myUtils.SaveFileToPublicFolder(file,"Images");
                
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

                //checking if we are updating item image or not
                var file = itemDTO.ImageFile;

                //updating image data
                if (file != null)
                {
                    var path=await  _myUtils.SaveFileToPublicFolder(file,"Images");
                    item.ImagePath = path;
                }
                
                //mapping (source: object,destination: object)
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
        
    }
}