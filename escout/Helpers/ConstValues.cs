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

        //Game Status
        public const int GS_HIDDEN = -1;
        public const int GS_PENDING = 0;
        public const int GS_ACTIVE = 1;
        public const int GS_FINISHED = 2;
        public const int GS_CANCELLED = 3;

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

        //Display Options
        public const string DO_USER_NAME = "userName";
        public const string DO_IMAGE_URL = "imageUrl";
        public const string DO_CLUB_NAME = "clubName";
        public const string DO_SPORT_NAME = "sportName";
        public const string DO_EVENT_NAME = "eventName";
        public const string DO_ATHLETE_NAME = "athleteName";
        public const string DO_COMPETITION_NAME = "competitionName";
        public const string DO_HOME_CLUB_NAME = "homeName";
        public const string DO_VISITOR_CLUB_NAME = "visitorName";
        public const string DO_HOME_IMAGE_URL = "homeImageUrl";
        public const string DO_VISITOR_IMAGE_URL = "visitorImageUrl";

        //Queries
        public const string QUERY = "SELECT * FROM {0} WHERE {1} {2} '{3}';";
        public const string QUERY_NOT_NULL = "SELECT * FROM {0} WHERE \"userId\"={1} AND \"{2}\" IS NOT NULL;";
        public const string QUERY_WITH_USER_ID = "SELECT * FROM {0} WHERE \"userId\"={1} AND {2} {3} '{4}';";

        //Other
        public const string DEFAULT_DATABASE_URL = "postgres://postgres:postgres@localhost:5432/postgres";
    }
}
