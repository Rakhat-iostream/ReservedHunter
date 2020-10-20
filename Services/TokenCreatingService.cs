using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Services
{
    public static class TokenCreatingService
    {
        public static string CreateToken(ClaimsIdentity identity, out string identityName, IConfiguration Configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            var token = new JwtSecurityToken(
                claims: identity.Claims,
                audience: AuthentificationOptions.AUDIENCE,
                issuer: AuthentificationOptions.ISSUER,
                expires: DateTime.Now.AddHours(AuthentificationOptions.LIFETIME),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );
            var handler = new JwtSecurityTokenHandler();
            var encodedToken = handler.WriteToken(token);
            identityName = identity.Name;
            return encodedToken;
        }

    }
}
