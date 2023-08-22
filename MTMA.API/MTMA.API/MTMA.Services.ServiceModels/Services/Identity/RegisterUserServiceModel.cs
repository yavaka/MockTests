namespace MTMA.Services.ServiceModels
{
    using MTMA.Data.Models.Identity;
    using MTMA.Services.Mapping;

    public class RegisterUserServiceModel : IMapTo<MTMAUser>
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
