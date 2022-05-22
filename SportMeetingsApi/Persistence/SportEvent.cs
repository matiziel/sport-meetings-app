using System;
using System.Collections.Generic;

namespace SportMeetingsApi.Persistence;
public class SportEvent {
    public int Id { get; set; }
    public User User { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int LimitOfParticipants { get; set; }
    public Location Location { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<SignUp> SignUps { get; set; } = new List<SignUp>();
}
