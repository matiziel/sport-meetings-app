using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMeetingsApi.Authentication.Settings;
using SportMeetingsApi.SportEvents.SignUps.Command;
using SportMeetingsApi.SportEvents.SignUps.Query;

namespace SportMeetingsApi.SportEvents.SignUps; 

[Route("api/[controller]")]
[ApiController]
public class SignUpsController : ControllerBase {
    
    private readonly SignUpsQueryService _signUpsQueryService;
    private readonly SignUpsService _signUpsService;

    public SignUpsController(SignUpsQueryService signUpsQueryService,
        SignUpsService signUpsService) {
        _signUpsQueryService = signUpsQueryService;
        _signUpsService = signUpsService;
    }

    [HttpGet("IsUserSignUp/{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<bool>> IsUserSignUp(int sportEventId) {
        return Ok(await _signUpsQueryService.IsUserSignUp(sportEventId));
    }
    
    [HttpPost("{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult> SignUp(int sportEventId) {
        if (await _signUpsQueryService.CanUserSignUp(sportEventId) == false)
            return BadRequest();
            
        await _signUpsService.CreateSignUp(sportEventId);
        return Ok();
    }

    [HttpDelete("{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult> SignOut(int sportEventId) {
        await _signUpsService.DeleteSignUp(sportEventId);
        return Ok();
    }
}