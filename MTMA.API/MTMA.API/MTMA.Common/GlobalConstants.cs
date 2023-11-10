namespace MTMA.Common
{
    public static class GlobalConstants
    {
        public class Admin
        {
            public const string RoleName = "Administrator";
        }

        public class Common
        {
            public const string SystemName = "MTMA";
            public const int Zero = 0;
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
            public const int MaxUrlLength = 2048;

            /// <summary>
            /// This internal error code is used to sign internal error message that must not be displayed to the user.
            /// </summary>
            public const string InternalErrorCode = "MTMA-Error:";
        }

        public class CommonValidationMessages
        {
            /// <summary>
            /// Generates a validation message indicating that the specified property is required.
            /// </summary>
            /// <param name="propertyName">The name of the property.</param>
            /// <returns>The validation message.</returns>
            public static string PropertyIsRequiredValidationMsg(string propertyName)
                => $"{propertyName} is required.";

            /// <summary>
            /// Generates a validation message indicating that the specified property cannot be empty.
            /// </summary>
            /// <param name="propertyName">The name of the property.</param>
            /// <returns>The validation message.</returns>
            public static string PropertyCannotBeEmptyValidationMsg(string propertyName)
                => $"{propertyName} cannot be empty.";

            /// <summary>
            /// Generates a validation message indicating that the specified property must be at least a certain length.
            /// </summary>
            /// <param name="propertyName">The name of the property.</param>
            /// <param name="minLength">The minimum length required.</param>
            /// <returns>The validation message.</returns>
            public static string PropertyMinLengthValidationMsg(string propertyName, int minLength)
                => $"{propertyName} must be at least {minLength} characters long.";

            /// <summary>
            /// Generates a validation message indicating that the specified property cannot exceed a certain length.
            /// </summary>
            /// <param name="propertyName">The name of the property.</param>
            /// <param name="maxLength">The maximum length allowed.</param>
            /// <returns>The validation message.</returns>
            public static string PropertyMaxLengthValidationMsg(string propertyName, int maxLength)
                => $"{propertyName} cannot exceed {maxLength} characters long.";
        }

        public class Identity
        {
            // Username
            public const int MinUsernameLength = 3;
            public const int MaxUsernameLength = 20;

            // Email
            public const int MinEmailLength = 3;
            public const int MaxEmailLength = 50;

            // Password
            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 32;
            public const string PasswordMustContainOneLowercaseLetter = "Password must contain at least one lowercase letter.";
            public const string PasswordMustContainOneUppercaseLetter = "Password must contain at least one uppercase letter.";
            public const string PasswordMustContainOneDigit = "Password must contain at least one digit.";
            public const string PasswordDoNotMatch = "Password do not match.";
        }
    }
}