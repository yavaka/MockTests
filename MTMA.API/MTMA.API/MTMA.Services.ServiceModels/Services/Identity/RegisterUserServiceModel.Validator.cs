namespace MTMA.Services.ServiceModels
{
    using FluentValidation;
    using static MTMA.Common.GlobalConstants.Identity;

    internal class RegisterUserServiceModelValidator : AbstractValidator<RegisterUserServiceModel>
    {
        public RegisterUserServiceModelValidator()
        {
            // Username
            this.RuleFor(u => u.UserName)
                .MinimumLength(MinUsernameLength).WithMessage($"Username must be at least {MinUsernameLength} characters long.")
                .MaximumLength(MaxUsernameLength).WithMessage($"Username cannot exceed {MaxUsernameLength} characters.")
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username cannot be empty.");

            // Email
            this.RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MinimumLength(MinEmailLength).WithMessage($"Email must be at least {MinEmailLength} characters long.")
                .MaximumLength(MaxEmailLength).WithMessage($"Email cannot exceed {MaxEmailLength} characters.")
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email cannot be empty.");

            // Password
            this.RuleFor(u => u.Password)
                .MinimumLength(MinPasswordLength).WithMessage($"Password must be at least {MinPasswordLength} characters long.")
                .MaximumLength(MaxPasswordLength).WithMessage($"Password cannot exceed {MaxPasswordLength} characters.")
                .Must(p => p.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter.")
                .Must(p => p.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter.")
                .Must(p => p.Any(char.IsDigit)).WithMessage("Password must contain at least one digit.")
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password cannot be empty.");

            // Confirm Password
            this.RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password).WithMessage("Password do not match.");
        }
    }
}
