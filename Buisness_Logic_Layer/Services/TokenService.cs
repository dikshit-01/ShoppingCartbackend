using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Logic_Layer.Services
{
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public TokenService(UserManager<User> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> BuildToken(string key, string issuer, User user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
             };

         var roles =await _userManager.GetRolesAsync(user);
        foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var securityKey = Encoding.ASCII.GetBytes(_configuration["JWT:key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);
           


            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                 SigningCredentials = credentials

            };

            var tokenHandeler = new JwtSecurityTokenHandler();

            var token = tokenHandeler.CreateToken(TokenDescriptor);
            var encryptedtoken = tokenHandeler.WriteToken(token);

            return encryptedtoken;

        }

        

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
