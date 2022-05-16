#nullable enable

namespace SportMeetingsApi.Persistence {
    public class User {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string? Name { get; set; }
    }
}