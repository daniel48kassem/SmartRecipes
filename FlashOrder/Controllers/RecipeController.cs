using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                // foreach (var singleIngredient in recipeDTO.Ingredients)
                // {
                //     singleIngredient.Id = ;
                // }


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
    }
}