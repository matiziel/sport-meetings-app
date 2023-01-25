using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMeetingsApi.Authentication.Settings;
using SportMeetingsApi.SportEvents.Events.Command;
using SportMeetingsApi.SportEvents.Events.Models;
using SportMeetingsApi.SportEvents.Events.Query;
using SportMeetingsApi.Utils;

namespace SportMeetingsApi.SportEvents.Events;

[Route("api/[controller]")]
[ApiController]
public class SportEventsController : ControllerBase {
    private readonly SportEventsQueryService _sportEventsQueryService;
    private readonly SportEventsService _sportEventsService;

    public SportEventsController(SportEventsQueryService sportEventsQueryService,
        SportEventsService sportEventsService) {
        _sportEventsQueryService = sportEventsQueryService;
        _sportEventsService = sportEventsService;
    }

    [HttpGet("user")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<IEnumerable<SportEventGet>>> GetEventsForUser() {
        return Ok(await _sportEventsQueryService.GetEventsCreatedByUser());
    }

    [HttpGet]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<IEnumerable<SportEventGet>>> GetEvents() {
        return Ok(await _sportEventsQueryService.GetEvents());
    }

    [HttpGet("{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<SportEventInfo>> GetEvent(int sportEventId) {
        return Ok(await _sportEventsQueryService.GetEvent(sportEventId));
    }

    [HttpPost]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<IEnumerable<SportEventGet>>> CreateEvent(
        [FromBody] SportEventCreate sportEventCreate) {
        await _sportEventsService.CreateSportEvent(sportEventCreate);
        return Ok();
    }
}