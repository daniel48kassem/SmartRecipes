using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public RecipeController(ILogger<RecipeController> logger,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            try
            {
            }

            catch (Exception e)
            {
                _logger.LogError(e, $"something went wrong in {nameof(GetRecipes)}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}