namespace MTMA.Services.Identity
{
    using MTMA.Services.ServiceModels;

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

        /// <summary>
        /// Changes the password for the user associated with the provided <paramref name="changePasswordRequest"/>.
        /// </summary>
        /// <param name="changePasswordRequest">The <see cref="ChangePasswordServiceModel"/> containing the necessary data for password change.</param>
        /// <returns>An <see cref="IdentityResultServiceModel"/> indicating the outcome of the password change operation.</returns>
        Task<IdentityResultServiceModel> ChangePassword(ChangePasswordServiceModel changePasswordRequest);
    }
}
