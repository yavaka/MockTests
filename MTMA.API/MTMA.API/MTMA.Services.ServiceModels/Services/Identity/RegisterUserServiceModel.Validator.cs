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
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(USERNAME))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(USERNAME))
                .MinimumLength(MinUsernameLength)
                    .WithMessage(PropertyMinLengthValidationMsg(USERNAME, MinUsernameLength))
                .MaximumLength(MaxUsernameLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(USERNAME, MaxUsernameLength));

            // Email
            this.RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(EMAIL))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(EMAIL))
                .EmailAddress()
                    .WithMessage("Please enter a valid email address.")
                .MinimumLength(MinEmailLength)
                    .WithMessage(PropertyMinLengthValidationMsg(EMAIL, MinEmailLength))
                .MaximumLength(MaxEmailLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(EMAIL, MaxEmailLength));

            // Password
            this.RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(PASSWORD))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(PASSWORD))
                .MinimumLength(MinPasswordLength)
                    .WithMessage(PropertyMinLengthValidationMsg(PASSWORD, MinPasswordLength))
                .MaximumLength(MaxPasswordLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(PASSWORD, MaxPasswordLength))
                .Must(p => p != null && p.Any(char.IsLower))
                    .WithMessage(PasswordMustContainOneLowercaseLetter)
                .Must(p => p != null && p.Any(char.IsUpper))
                    .WithMessage(PasswordMustContainOneUppercaseLetter)
                .Must(p => p != null && p.Any(char.IsDigit))
                    .WithMessage(PasswordMustContainOneDigit);

            // Confirm Password
            this.RuleFor(u => u.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg("Confirm Password"))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg("Confirm Password"))
                .Equal(u => u.Password)
                    .WithMessage(PasswordDoNotMatch);
        }
    }
}