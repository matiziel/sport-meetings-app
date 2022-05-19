using System;

namespace SportMeetingsApi.Authentication.Models;

public record TokenModel(
    string AccessToken, string RefreshToken, DateTime Expiration
);
