namespace escout.Helpers
{
    public class ConstValues
    {
        //Access Levels
        public const int AL_UNAUTHORIZED = -1;
        public const int AL_ADMINISTRATOR = 0;
        public const int AL_MODERATOR = 1;
        public const int AL_MANAGER = 2;
        public const int AL_USER = 3;

        //Messages
        public const string MSG_WRONG_PASSWORD = "Password is wrong.";
        public const string MSG_ACCOUNT_NOT_FOUND = "Account does not exist.";
        public const string MSG_ACCOUNT_UNAUTHORIZED = "Account unauthorized.";
        public const string MSG_USERNAME_USED = "Username in use";
        public const string MSG_EMAIL_USED = "Email in use";

        //Notifications
        public const string NTF_TITLE_GENERIC = "eScout Notification";
        public const string NTF_TITLE_PASSWORD = "eScout Password Notification";
        public const string NTF_BODY_NEW_ACCOUNT = "Welcome to eScout {0}";
        public const string NTF_BODY_PASSWORD_RESET = "Your new eScout password: {0}";

        //Queries
        public const string QUERY = "SELECT * FROM {0} WHERE {1} {2} '{3}';";
        public const string QUERY_NOT_NULL = "SELECT * FROM {0} WHERE \"userId\"={1} AND \"{2}\" IS NOT NULL;";
        public const string QUERY_WITH_USER_ID = "SELECT * FROM {0} WHERE \"userId\"={1} AND {2} {3} '{4}';";

        //Other
        public const string DEFAULT_DATABASE_URL = "postgres://postgres:password@localhost:5432/postgres";
    }
}
