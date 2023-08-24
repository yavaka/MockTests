namespace MTMA.Services.ServiceModels
{
    using FluentValidation;
    using static MTMA.Common.GlobalConstants.CommonValidationMessages;
    using static MTMA.Common.GlobalConstants.Identity;

    internal class RegisterUserServiceModelValidator : AbstractValidator<RegisterUserServiceModel>
    {
        private const string USERNAME = "Username";
        private const string EMAIL = "Email";
        private const string PASSWORD = "Password";

        public RegisterUserServiceModelValidator()
        {
            // Username
            this.RuleFor(u => u.Username)
                .MinimumLength(MinUsernameLength)
                    .WithMessage(PropertyMinLengthValidationMsg(USERNAME, MinUsernameLength))
                .MaximumLength(MaxUsernameLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(USERNAME, MaxUsernameLength))
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(USERNAME))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(USERNAME));

            // Email
            this.RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage("Please enter a valid email address.")
                .MinimumLength(MinEmailLength)
                    .WithMessage(PropertyMinLengthValidationMsg(EMAIL, MinEmailLength))
                .MaximumLength(MaxEmailLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(EMAIL, MaxEmailLength))
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(EMAIL))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(EMAIL));

            // Password
            this.RuleFor(u => u.Password)
                .MinimumLength(MinPasswordLength)
                    .WithMessage(PropertyMinLengthValidationMsg(PASSWORD, MinPasswordLength))
                .MaximumLength(MaxPasswordLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(PASSWORD, MaxEmailLength))
                .Must(p => p.Any(char.IsLower))
                    .WithMessage(PasswordMustContainOneLowercaseLetter)
                .Must(p => p.Any(char.IsUpper))
                    .WithMessage(PasswordMustContainOneUppercaseLetter)
                .Must(p => p.Any(char.IsDigit))
                    .WithMessage(PasswordMustContainOneDigit)
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(PASSWORD))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(PASSWORD));

            // Confirm Password
            this.RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password)
                    .WithMessage(PasswordDoNotMatch);
        }
    }
}
