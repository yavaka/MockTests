namespace MTMA.Services.JwtGenerator
{
    using MTMA.Data.Models.Identity;

    /// <summary>
    /// Interface representing the service responsible for generating JSON Web Tokens (JWT) for users.
    /// </summary>
    public interface IJwtGeneratorService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the JWT token will be generated.</param>
        /// <returns>Generated JWT token as a string.</returns>
        Task<string> GenerateToken(MTMAUser user);
    }
}
