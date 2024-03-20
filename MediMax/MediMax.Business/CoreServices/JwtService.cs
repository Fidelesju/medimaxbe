using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Data.ApplicationModels;

namespace MediMax.Business.CoreServices
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;

        public JwtService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


            public string GetJwtToken(string nameIdentifier, string role)
            {
                JwtSecurityTokenHandler tokenHandler;
                byte[] key;
                SecurityToken securityToken;
                SecurityTokenDescriptor tokenDescriptor;
                string jwtToken;

                tokenHandler = new JwtSecurityTokenHandler();
                key = Encoding.ASCII.GetBytes(_appSettings.Segredo);

                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, nameIdentifier.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            }),
                    Expires = DateTime.UtcNow.AddYears(100),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                securityToken = tokenHandler.CreateToken(tokenDescriptor);
                jwtToken = tokenHandler.WriteToken(securityToken);

                return jwtToken;
            }
        }
}
