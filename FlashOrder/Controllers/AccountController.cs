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
using FlashOrder.Services.Auth;
using FlashOrder.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly UserManager<ApiUser> _userManager;

        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;


        public AccountController(ILogger<AccountController> logger,
            UserManager<ApiUser> userManager, IAuthManager authManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registrationData)
        {
            _logger.LogInformation($"registration attempt For {registrationData.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<RegisterDTO, ApiUser>(registrationData);
                
                var result = await _userManager.CreateAsync(user,registrationData.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code,error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(loginDto))
                {
                    return Unauthorized();
                }

                return Accepted(new {Token = await _authManager.CreateToken()});
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}