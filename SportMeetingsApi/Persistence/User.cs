using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SportMeetingsApi.Persistence;

public class User : IdentityUser {
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<SportEvent> CreatedSportEvents { get; set; } = new List<SportEvent>();
    public ICollection<SignUp> SignUps { get; set; } = new List<SignUp>();
}
