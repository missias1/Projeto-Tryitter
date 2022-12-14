using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tryitter.Autentication
{
    public class TokenGenerator
    {
        public string Generate(string nome, string email, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(nome, email, id),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstant.Secret)),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Expires = DateTime.Now.AddDays(7)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public ClaimsIdentity AddClaims(string nome, string email, int id)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim("Email", email));
            claims.AddClaim(new Claim("Nome", nome));
            claims.AddClaim(new Claim("Id", id.ToString()));

            return claims;
        }
    }
}
