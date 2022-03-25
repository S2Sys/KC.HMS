namespace KC.HMS.Core.Settings
{
    public class ApplicationConstants
    {
    
        public const string DEFAULT_PASSWORD = "!QAZ1qaz";
        public const RoleKind DEFAULT_ROLE = RoleKind.Guest;
        public const string EmptyGuidString = "00000000-0000-0000-0000-000000000000";

        public const string AuthenticationScheme = "GSPResidency";
        public const string AuthenticationAPITokenScheme = "APIToken";

    }

    public class VALIDATION_MESSGAE
    {


        public const string REQUIRED = "The {0} is required";
        public const string MAXLENGTH = "The {0} value cannot exceed {1} characters.";
    }

    public class JwtAuthenticationDefaults
    {
        public const string AuthenticationScheme = "JWT";
        public const string HeaderName = "Authorization";
        public const string BearerPrefix = "Bearer";
    }

}
