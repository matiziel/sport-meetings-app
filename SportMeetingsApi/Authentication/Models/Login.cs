using System.ComponentModel.DataAnnotations;

namespace SportMeetingsApi.Authentication.Models;

public record Login(
    [Required(ErrorMessage = "User Name is required")]
    string Username,
    [Required(ErrorMessage = "Password is required")]
    string Password
);
