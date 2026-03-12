using CursosOnline.Domains;
using CursosOnline.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursosOnline.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        public class GeradorTokenJwt
        {
            private readonly IConfiguration _config;

            public GeradorTokenJwt(IConfiguration config)
            {
                _config = config;
            }

            public string GerarToken(Instrutor instrutor)
            {
                var key = _config["Jwt:Key"]!;
                var issuer = _config["Jwt:Issuer"]!;
                var audience = _config["Jwt:Audience"]!;
                var ExpiraMinutos = int.Parse(_config["Jwt:ExpiraMinutos"]!);
                var keyBytes = Encoding.UTF8.GetBytes(key);
                if (keyBytes.Length < 32) throw new DomainException("Token inválido");
                var securityKey = new SymmetricSecurityKey(keyBytes);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, instrutor.Id.ToString()),
                new Claim(ClaimTypes.Name, instrutor.Nome),
                new Claim(ClaimTypes.Email, instrutor.Email),
            };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(ExpiraMinutos),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }




        }
    }
}
