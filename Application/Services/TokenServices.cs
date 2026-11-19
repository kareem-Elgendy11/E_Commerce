using Application.Interfaces.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public TokenServices(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<(string token,DateTime expireAt)> GenerateAccessToken(ApplicationUser User)
        {

            var random = Guid.NewGuid().ToString();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]!));
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Name, User.UserName!));
            var roles = await userManager.GetRolesAsync(User);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var signincred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["jwt:issuer"],
                audience: configuration["jwt:Audience"],
                expires:DateTime.Now.AddMinutes(int.Parse(configuration["jwt:ExpireAt"]!)),
                claims:claims,
                signingCredentials:signincred
                );
            return (new JwtSecurityTokenHandler().WriteToken(token),token.ValidTo.ToLocalTime());
        }

        public string GenerateRefreshToken()
        {
            var token = RandomNumberGenerator.GetBytes(64);
            var refreshtoken = Convert.ToBase64String(token);
           return refreshtoken;
        }
    }
}
