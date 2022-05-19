using System;
using Microsoft.AspNetCore.Identity;

namespace SportMeetingsApi.Persistence;

public class User : IdentityUser {
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
