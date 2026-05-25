using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TrackExences.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config) => _config = config;

    public string  GenerateToken(string userId, string email, string name,string role)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? ""));
        
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Name, name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role),
            
        ];
        
        var tokenHandler = new JwtSecurityToken(
            issuer: jwtSettings["issuer"],
            audience: jwtSettings["audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["exp"])),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        
        return new JwtSecurityTokenHandler().WriteToken(tokenHandler);
    }
}