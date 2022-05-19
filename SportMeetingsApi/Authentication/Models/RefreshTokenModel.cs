using System.ComponentModel.DataAnnotations;

namespace SportMeetingsApi.Authentication.Models;

public record RefreshTokenModel(
    [Required(ErrorMessage = "AccessToken is required")]
    string AccessToken,
    [Required(ErrorMessage = "RefreshToken is required")]
    string RefreshToken
);
