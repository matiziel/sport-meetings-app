using System;
using System.Collections.Generic;

namespace SportMeetingsApi.Persistence;
public class SportEvent {
    public int Id { get; set; }
    public User Owner { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Location { get; set; } = default!;
    public int LimitOfParticipants { get; set; }
    public DateTime StartDate { get; set; }
    public int DurationInHours { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<SignUp> SignUps { get; set; } = new List<SignUp>();
}
