using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        public RecipeController(ILogger<RecipeController> logger, IMapper mapper, IUnitOfWork unitOfWork,UserManager<ApiUser> userManager,IAuthorizationService authorizationService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery]RecipeParameters recipeParameters)
        {
            try
            {
                var recipes=await _unitOfWork.Recipes.GetAllWithFilters(null
                    ,null,new List<string> {"Ingredients.Item","Chef"},recipeParameters);

                var results = _mapper.Map<List<RecipeDTO>>(recipes);
                return Ok(results);
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(GetRecipes)}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDTO recipeDTO)
        {
            
            
            if (!ModelState.IsValid)
            {
                _logger.LogError($"invalid post attempt{nameof(CreateRecipe)}");
                return BadRequest(ModelState);
            }

            try
            {

                //get the authenticated user
                var email = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(email);
                
                //set the chef of this recipe to be the current user
                var recipe = _mapper.Map<Recipe>(recipeDTO);
                recipe.ChefId = user.Id;
                
                //we are altering the database so we have to commit changes
                await _unitOfWork.Recipes.Insert(recipe);
                await _unitOfWork.save();

                return CreatedAtRoute("GetRecipe", new {id = recipe.Id}, recipe);
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(CreateRecipe)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"invalid post attempt{nameof(GetRecipe)}");
                return NotFound("Your Recipe cannot be found");
            }

            try
            {
                var recipe = await _unitOfWork.Recipes.Get(q => q.Id == id,
                    new List<string> {"Ingredients.Item","Chef"});
                var res = _mapper.Map<RecipeDTO>(recipe);
                return Ok(res);
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(CreateRecipe)}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPut("{id:int}", Name = "UpdateRecipe")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int id,[FromBody] UpdateRecipeDTO recipeDto)
        {
            if (id<1||!ModelState.IsValid)
            {
                _logger.LogError($"invalid post attempt{nameof(UpdateRecipe)}");
                return BadRequest("your data is invalid");
            }
           
            
            try
            {
                var recipe=await _unitOfWork.Recipes.Get(q=>q.Id==id);

                if (!  _authorizationService.AuthorizeAsync(User, recipe, "CreatorChefPolicy").Result.Succeeded)
                {
                    return Unauthorized("You are Not Allowed To Perform This Action");
                }

                if (recipe == null)
                {
                    _logger.LogError($"invalid Update attempt in {nameof(UpdateRecipe)}");
                    return BadRequest("Submitted Data s not valid");
                }

                //mapping (source: object,destination: object)
                _mapper.Map(recipeDto,recipe);
                _unitOfWork.Recipes.Update(recipe);
                await _unitOfWork.save();
                
                return NoContent();
            }
        
            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(UpdateRecipe)}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}