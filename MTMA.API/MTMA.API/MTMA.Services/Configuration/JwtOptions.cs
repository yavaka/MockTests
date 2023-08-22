namespace MTMA.Services.Configuration
{
    public class JwtOptions
    {
        public const string Jwt = "Jwt";

        public string Secret { get; set; } = string.Empty;
    }
}
