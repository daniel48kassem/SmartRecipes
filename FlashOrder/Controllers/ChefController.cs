using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using FlashOrder.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class ChefController : ControllerBase
    {
        private readonly ILogger<ChefController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IUnitOfWork _unitOfWork;
        
        public ChefController(ILogger<ChefController> logger,
            UserManager<ApiUser> userManager, IAuthManager authManager, IMapper mapper
            ,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetChefs()
        {
            try
            {
                //get the chefs
                var chefs =await  _userManager.GetUsersInRoleAsync("Chef");
                
                var res = _mapper.Map<List<UserDTO>>(chefs);
                return Ok(res);
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetChefs)}");
                return Problem($"Something went wrong in the {nameof(GetChefs)}", statusCode: 500);
            }
        }
        
        
        [HttpGet]
        [Route("Followers/{id:Guid}")]
        public async Task<IActionResult> GetFollowers(Guid id)
        {
            try
            {
                /*
                 get the chef first
                 then get his follow relations
                 then extract the follower objects from this relations
                */
                var followers =await  _userManager.Users.Where(u => u.Id == id.ToString())
                    .SelectMany(u => u.ChefFollowers)
                    .Select(u=>u.Follower)
                    .ToListAsync();
                
                var res = _mapper.Map<List<UserDTO>>(followers);
                return Ok(res);
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetFollowers)}");
                return Problem($"Something went wrong in the {nameof(GetFollowers)}", statusCode: 500);
            }
        }
        
    }
}