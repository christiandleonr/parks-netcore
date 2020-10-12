using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Parks.Models;

namespace Parks.Utils
{
    public class Util
    {
        private readonly IConfiguration _configuration;
        public Util(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetToken(Employee employee)
        {
            // Leemos el secret_key desde nuestro appsetting
            var secretKey = _configuration["Auth:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Creamos los claims (pertenencias, caracteristicas) del usuario
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id),
                new Claim(ClaimTypes.Email, employee.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretkey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }    
}