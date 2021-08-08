using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using FlashOrder.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly UserManager<ApiUser> _userManager;

        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IUnitOfWork _unitOfWork;


        public UserController(ILogger<UserController> logger,
            UserManager<ApiUser> userManager, IAuthManager authManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Follow-Chef")]
        [Authorize]
        public async Task<IActionResult> FollowChef([FromBody] FollowDTO followDto)
        {
            try
            {
                //Validation oF The Request Followed Chef
                var chef = await _userManager.FindByIdAsync(followDto.ChefId);
                if (chef == null)
                {
                    _logger.LogError($"something went wrong in {nameof(FollowChef)}");
                    return BadRequest($"This Chef is not exist");
                }

                //get the current user
                var email = User.FindFirstValue(ClaimTypes.Email);
                var currentUser = await _userManager.FindByEmailAsync(email);

                if (currentUser == null)
                {
                    return Unauthorized();
                }

                //create the follow relation
                Follow followRelation = new Follow
                    {Chef = chef, ChefId = chef.Id, Follower = currentUser, FollowerId = currentUser.Id};
                //insert to database
                await _unitOfWork.Follows.Insert(followRelation);
                await _unitOfWork.save();

                // currentUser.FollowedChefs.Add(followRelation);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(FollowChef)}");
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }
    }
}