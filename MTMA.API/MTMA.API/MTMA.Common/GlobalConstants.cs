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
        }
    }
}