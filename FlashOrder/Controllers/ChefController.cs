// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using AutoMapper;
// using FlashOrder.Data;
// using FlashOrder.DTOs;
// using FlashOrder.Services.Auth;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
//
// namespace FlashOrder.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ChefController : ControllerBase
//     {
//         private readonly ILogger<ChefController> _logger;
//
//         private readonly UserManager<ApiUser> _userManager;
//
//         private readonly IMapper _mapper;
//         private readonly IAuthManager _authManager;
//
//
//         public ChefController(ILogger<ChefController> logger,
//             UserManager<ApiUser> userManager, IAuthManager authManager, IMapper mapper)
//         {
//             _logger = logger;
//             _userManager = userManager;
//             _mapper = mapper;
//             _authManager = authManager;
//         }
//         
//         [HttpGet("{id:string}")]
//         public async Task<IActionResult> GetFollowers(string id)
//         {
//
//             try
//             {
//                 var user = await _userManager.FindByIdAsync(id);
//
//                 // var followers =  user.ChefFollowers
//                 
//                 // var followers = await _unitOfWork.Recipes.Get(q => q.Id == id,
//                 //     new List<string> {"Ingredients.Item","Chef","Steps"});
//                 
//                 var res = _mapper.Map<UserDTO>(followers);
//                 return Ok(res);
//             }
//
//             catch (Exception e)
//             {
//                 _logger.LogError(e, $"Something went wrong in the {nameof(GetFollowers)}");
//                 return Problem($"Something went wrong in the {nameof(GetFollowers)}", statusCode: 500);
//             }
//         }
//
//
//         
//         
//     }
//     
// }