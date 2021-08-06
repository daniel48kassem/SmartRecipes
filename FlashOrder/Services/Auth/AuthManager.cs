using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlashOrder.Data;
using FlashOrder.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FlashOrder.Services.Auth
{
    public class AuthManager:IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private  ApiUser _user;

       public AuthManager(UserManager<ApiUser> userManager,IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        
        public async Task<bool> ValidateUser(LoginDTO loginDto)
        {
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            Console.WriteLine(_user.Email);
            Console.WriteLine(_user.PasswordHash);
            // Console.WriteLine(loginDto.Password);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password));
        }

        public async Task<string> CreateToken()
        {
            var claims = new[] {    
                new Claim(ClaimTypes.Name, _user.UserName),
                // new Claim(ClaimTypes.Role, _user.),
                new Claim(ClaimTypes.NameIdentifier,
                    Guid.NewGuid().ToString())
            };


            var jwtSettings = _configuration.GetSection("JWT");
            
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value));
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            
            var tokenDescriptor =  new JwtSecurityToken(jwtSettings.GetSection("Issuer").Value,
                claims:claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("TokenExpirationDuration").Value)),
                signingCredentials: credentials); 
            
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);  
        }
    }
}