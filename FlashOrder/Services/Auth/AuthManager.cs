using System;
using System.Collections;
using System.Collections.Generic;
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
            // Console.WriteLine(_user.Email);
            // Console.WriteLine(_user.PasswordHash);
            // Console.WriteLine(loginDto.Password);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password));
        }

        public async Task<string> CreateToken()
        {
            //this claims will be available in User in All Controllers,you will need them

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.NameIdentifier,
                    _user.Id),
                new Claim(ClaimTypes.Email, _user.Email)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            
            var jwtSettings = _configuration.GetSection("JWT");
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value));
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // var lifeTime =DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("TokenExpirationDuration").Value)) ;
            var lifeTime =DateTime.Now.AddMinutes(Convert.ToDouble(4546545)) ;

            var tokenDescriptor =  new JwtSecurityToken(jwtSettings.GetSection("Issuer").Value,
                claims:claims, 
                expires: lifeTime,
                signingCredentials: credentials); 
            
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);  
        }
    }
}