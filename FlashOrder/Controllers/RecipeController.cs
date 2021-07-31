using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
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

        public RecipeController(ILogger<RecipeController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery]RecipeParameters recipeParameters)
        {
            try
            {
                // var model = business.GetProducts(searchModel);
                //
                // var recipes = await _unitOfWork.Recipes.Filter();

                var recipes=await _unitOfWork.Recipes.GetAllWithFilters(null
                    ,null,new List<string> {"Ingredients.Item"},recipeParameters);

                // var q = await _unitOfWork.Recipes.GetAllWith(q => q.Title.Contains(searchString)
                //     , null, new List<string> {"Ingredients.Item"});
                //
                // var q2 = QuerySpecificationExtensions.Specify(q,
                //     new RecipeContainsIngredients(new List<string>() {"Caviar"}));

                // var recipes = await q2.AsNoTracking().ToListAsync();
                
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
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDTO recipeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"invalid post attempt{nameof(CreateRecipe)}");
                return BadRequest(ModelState);
            }

            try
            {
                var recipe = _mapper.Map<Recipe>(recipeDTO);
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
                    new List<string> {"Ingredients.Item"});
                var res = _mapper.Map<RecipeDTO>(recipe);
                return Ok(res);
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(CreateRecipe)}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}