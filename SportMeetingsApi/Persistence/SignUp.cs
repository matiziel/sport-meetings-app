namespace SportMeetingsApi.Persistence;

public class SignUp {
    public int Id { get; set; }
    public User User { get; set; } = default!;
    public SportEvent SportEvent { get; set; } = default!;
}
