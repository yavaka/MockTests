namespace MTMA.Services.Identity
{
    using MTMA.Services.ServiceModels;
    using MTMA.Services.ServiceModels.Services.Identity;

    /// <summary>
    /// Interface representing the service responsible for identity-related operations.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="serviceModel">The service model containing user registration information.</param>
        /// <returns>An instance of <see cref="IdentityResultServiceModel"/> representing the result of the registration operation.</returns>
        Task<IdentityResultServiceModel> Register(RegisterUserServiceModel serviceModel);

        /// <summary>
        /// Attempts to log in a user using the provided login service model.
        /// </summary>
        /// <param name="serviceModel">The service model containing login credentials.</param>
        /// <returns>
        /// The result is a tuple containing:
        /// <para>- An <see cref="IdentityResultServiceModel"/> indicating the result of the login operation.</para>
        /// <para>- A string representing the generated JWT token upon successful login.</para>
        /// </returns>
        Task<(IdentityResultServiceModel, string)> Login(LoginUserServiceModel serviceModel);
    }
}
