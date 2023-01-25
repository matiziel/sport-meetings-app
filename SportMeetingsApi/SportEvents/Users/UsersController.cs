using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMeetingsApi.Authentication.Settings;
using SportMeetingsApi.SportEvents.Users.Models;
using SportMeetingsApi.SportEvents.Users.Query;

namespace SportMeetingsApi.SportEvents.Users; 

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly UsersQueryService _usersQueryService;

    public UsersController(UsersQueryService usersQueryService) {
        _usersQueryService = usersQueryService;
    }

    [HttpGet]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<UserInfo>> GetUserInfo() {
        return Ok(await _usersQueryService.GetUserInfo());
    }
    
}