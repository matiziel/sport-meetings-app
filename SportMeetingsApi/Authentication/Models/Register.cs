using System.ComponentModel.DataAnnotations;

namespace SportMeetingsApi.Authentication.Models;

public record Register(
    [Required(ErrorMessage = "User Name is required")]
    string Username,
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    string Email,
    [Required(ErrorMessage = "Password is required")]
    string Password
);