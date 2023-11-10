namespace MTMA.Services.Tests
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Moq;
    using MTMA.Data.Models.Identity;
    using MTMA.Services.Configuration;
    using MTMA.Services.JwtGenerator;
    using Xunit;

    public class JwtGeneratorServiceTests
    {
        private readonly JwtGeneratorService _jwtGeneratorService;
        private readonly Mock<UserManager<MTMAUser>> _userManager;

        public JwtGeneratorServiceTests()
        {
            var jwtOptions = new JwtOptions
            {
                Secret = "S3cR3TtM3sSaGe3eM@g1!1!CcS3cR3TtM3sSaGe3eM@g1!1!CcS3cR3TtM3sSaGe3eM@g1!1!Cc"
            };

            _userManager = new Mock<UserManager<MTMAUser>>(
                Mock.Of<IUserStore<MTMAUser>>(), default!, default!, default!, default!, default!, default!, default!, default!);

            _jwtGeneratorService = new JwtGeneratorService(
                new OptionsWrapper<JwtOptions>(jwtOptions),
                _userManager.Object);
        }

        [Fact]
        public async Task GenerateToken_ShouldReturnAJwtToken()
        {
            var user = new MTMAUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@example.com"
            };

            _userManager.Setup(u => u.IsInRoleAsync(user, It.IsAny<string>())).ReturnsAsync(true);

            var token = await _jwtGeneratorService.GenerateToken(user);

            Assert.NotNull(token);
            Assert.IsType<string>(token);
        }
    }
}
