using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.Filters.ActionFilters;
using FlashOrder.IRepository;
using FlashOrder.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ServiceFilter(typeof(EnsureChefExists))]
        public async Task<IActionResult> FollowChef([FromBody] FollowDTO followDto)
        {
            try
            {
                // var chef = await _userManager.FindByIdAsync(followDto.ChefId);
                var chef = HttpContext.Items["chef"] as ApiUser;

                // if (chef == null)
                // {
                //     _logger.LogError($"something went wrong in {nameof(FollowChef)}");
                //     return BadRequest($"This Chef is not exist");
                // }

                //get the current user
                var email = User.FindFirstValue(ClaimTypes.Email);
                var currentUser = await _userManager.Users.Where(u => u.Email == email)
                    .Include(u => u.FollowedChefs)
                    .FirstOrDefaultAsync();

                if (currentUser == null)
                {
                    return Unauthorized();
                }

                //check if there is  a follow relation before
                bool isAlreadyFollowedThisChef = currentUser.FollowedChefs.Any(f => f.ChefId == followDto.ChefId);
                if (isAlreadyFollowedThisChef)
                {
                    return BadRequest($"You Are Already Following this Chef");
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

        [HttpPost]
        [Route("UnFollow-Chef")]
        [Authorize]
        [ServiceFilter(typeof(EnsureChefExists))]
        public async Task<IActionResult> UnFollowChef([FromBody] FollowDTO followDto)
        {
            try
            {
                //get the current user
                var email = User.FindFirstValue(ClaimTypes.Email);
                var currentUser = await _userManager.Users.Where(u => u.Email == email)
                    .Include(u => u.FollowedChefs)
                    .FirstOrDefaultAsync();

                if (currentUser == null)
                {
                    return Unauthorized();
                }

                var chef = HttpContext.Items["chef"] as ApiUser;
                // var chef = await _userManager.FindByIdAsync(followDto.ChefId);
                // if (chef == null)
                // {
                //     _logger.LogError($"something went wrong in {nameof(FollowChef)}");
                //     return BadRequest($"This Chef is not exist");
                // }

                //get the relation if exist
                var followRelation = currentUser.FollowedChefs
                    .FirstOrDefault(f => f.ChefId == followDto.ChefId);

                //check if there is  a follow relation before
                if (followRelation == null)
                {
                    return BadRequest($"you are trying to unfollow a chef that you dont previously follow");
                }

                //delete the relation from the database
                await _unitOfWork.Follows.Delete(followRelation);
                await _unitOfWork.save();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(UnFollowChef)}");
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }

        [HttpPost]
        [Route("RateRecipe/{id:int}")]
        //Validate if The Recipe exist using filter
        [ServiceFilter(typeof(EnsureRecipeExists))]
        //check if there is a rating relation before
        [ServiceFilter(typeof(EnsureRatingRelationNotExists))]
        [Authorize]
        public async Task<IActionResult> RateRecipe(int id, [FromBody] RateDTO rateDto)
        {
            try
            {
                //get recipe from parameters,(this came from our filter)
                var recipe = HttpContext.Items["recipe"] as Recipe;

                //get the current user
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                //create the Rating relation
                var ratingRelation = new Rating
                    {RecipeId = recipe.Id, UserId = currentUserId, Value = rateDto.Value};

                //insert to database
                await _unitOfWork.Ratings.Insert(ratingRelation);
                await _unitOfWork.save();

                recipe.IsRatingUpdated = true;

                //update the recipe to indicate that it should be rerated later by the background service
                _unitOfWork.Recipes.Update(recipe);
                await _unitOfWork.save();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(RateRecipe)}");
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }
    }
}