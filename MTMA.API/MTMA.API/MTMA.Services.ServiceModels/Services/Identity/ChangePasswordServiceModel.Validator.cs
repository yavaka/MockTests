namespace MTMA.Services.ServiceModels
{
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;
    using static MTMA.Common.GlobalConstants.Common;
    using static MTMA.Common.GlobalConstants.CommonValidationMessages;
    using static MTMA.Common.GlobalConstants.Identity;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "Reviewed")]
    internal class ChangePasswordServiceModelValidator : AbstractValidator<ChangePasswordServiceModel>
    {
        private const string NEW_PASSWORD = "New Password";

        public ChangePasswordServiceModelValidator()
        {
            // User Id
            this.RuleFor(model => model.UserId)
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg($"{InternalErrorCode} user id"))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg($"{InternalErrorCode} user id"));

            // Current
            this.RuleFor(model => model.CurrentPassword)
                .NotNull().NotEmpty()
                    .WithMessage(PropertyIsRequiredValidationMsg("Current Password"));

            // New Password
            this.RuleFor(model => model.NewPassword)
                .MinimumLength(MinPasswordLength)
                    .WithMessage(PropertyMinLengthValidationMsg(NEW_PASSWORD, MinPasswordLength))
                .MaximumLength(MaxPasswordLength)
                    .WithMessage(PropertyMaxLengthValidationMsg(NEW_PASSWORD, MaxPasswordLength))
                .Must(p => p.Any(char.IsLower))
                    .WithMessage($"New {PasswordMustContainOneLowercaseLetter}")
                .Must(p => p.Any(char.IsUpper))
                    .WithMessage($"New {PasswordMustContainOneUppercaseLetter}")
                .Must(p => p.Any(char.IsDigit))
                    .WithMessage($"New {PasswordMustContainOneDigit}")
                .NotNull()
                    .WithMessage(PropertyIsRequiredValidationMsg(NEW_PASSWORD))
                .NotEmpty()
                    .WithMessage(PropertyCannotBeEmptyValidationMsg(NEW_PASSWORD));

            // Confirm New Password
            this.RuleFor(model => model.ConfirmNewPassword)
                .Equal(model => model.NewPassword)
                    .WithMessage($"New {PasswordDoNotMatch}");
        }
    }
}
