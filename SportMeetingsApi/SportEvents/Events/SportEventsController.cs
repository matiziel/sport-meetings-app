using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMeetingsApi.Authentication.Settings;
using SportMeetingsApi.SportEvents.Events.Models;
using SportMeetingsApi.SportEvents.Events.Query;
using SportMeetingsApi.Utils;

namespace SportMeetingsApi.SportEvents.Events;

[Route("api/[controller]")]
[ApiController]
public class SportEventsController : ControllerBase {
    private readonly SportEventsQueryService _sportEventsQueryService;
    public SportEventsController(SportEventsQueryService sportEventsQueryService)
    {   
        _sportEventsQueryService = sportEventsQueryService;
    } 

    [HttpGet("user")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<IEnumerable<SportEventGet>>> GetEventsForUser([FromQuery] Paging.Page page)
    {
        var paging = Paging.Create(page);
        return Ok(await _sportEventsQueryService.GetEventsCreatedByUser(paging));
    }

    [HttpGet]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<IEnumerable<SportEventGet>>> GetEvents([FromQuery] Paging.Page page)
    {
        var paging = Paging.Create(page);
        return Ok(await _sportEventsQueryService.GetEvents(paging));
    } 


}


