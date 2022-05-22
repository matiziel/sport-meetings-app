using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMeetingsApi.Authentication.Models;
using SportMeetingsApi.Authentication.Services;
using SportMeetingsApi.Authentication.Settings;

namespace SportMeetingsApi.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase {
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService) {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(Login login)
    => (await _authenticationService.Login(login))
        .Match<ActionResult>(
            Left: l => Unauthorized(l),
            Right: r => Ok(r)
        );

    [HttpPost("register")]
    public async Task<ActionResult> Register(Register register)
    => (await _authenticationService.Register(register))
        .Match<ActionResult>(
            Left: l => Unauthorized(l),
            Right: r => Ok(r)
        );

    [HttpPost("refreshToken")]
    public async Task<ActionResult> RefreshToken(RefreshTokenModel refreshTokenModel)
    => (await _authenticationService.RefreshToken(refreshTokenModel))
        .Match<ActionResult>(
            Left: l => Unauthorized(l),
            Right: r => Ok(r)
        );

    [Authorize(Roles = UserRole.Admin)]
    [HttpGet("test")]
    public async Task<ActionResult> Test() => Ok(await _authenticationService.Test());
}
