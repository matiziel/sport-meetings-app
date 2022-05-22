namespace SportMeetingsApi.Authentication.Settings;

public static class UserRole {
    public const string Admin = "Admin";
    public const string User = "User";

    public static string GetRoles(params string[] roles) {
        return string.Join(", ", roles);
    }
}