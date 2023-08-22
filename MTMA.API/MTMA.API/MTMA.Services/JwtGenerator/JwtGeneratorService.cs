namespace MTMA.Services.JwtGenerator
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using MTMA.Data.Models.Identity;
    using MTMA.Services.Configuration;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using AdminConstants = MTMA.Common.GlobalConstants.Admin;

    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<MTMAUser> _userManager;

        public JwtGeneratorService(
            IOptions<JwtOptions> jwtOptions,
            UserManager<MTMAUser> userManager)
        {
            this._jwtOptions = jwtOptions.Value;
            this._userManager = userManager;
        }

        public async Task<string> GenerateToken(MTMAUser user)
        {
            // Initialize a token handler and retrieve the secret key for JWT generation
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            // Configure the token descriptor with user claims, expiration, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email!)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Check if the user is an administrator and add a role claim if applicable
            var isAdministrator = await this._userManager.IsInRoleAsync(user, AdminConstants.RoleName);
            if (isAdministrator)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, AdminConstants.RoleName));
            }

            // Create the JWT token and write it as a string
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
