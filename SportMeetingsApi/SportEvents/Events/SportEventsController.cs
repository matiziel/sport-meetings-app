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

    [HttpGet]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<IEnumerable<SportEventGet>>> GetEvents() =>
        Ok(await _sportEventsQueryService.GetEvents());

    [HttpGet("IsUserEventOwner/{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<bool>> IsUserEventOwner(int sportEventId) =>
        Ok(await _sportEventsQueryService.IsUserOwnerOfEvent(sportEventId));

    [HttpGet("{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<SportEventInfo>> GetEvent(int sportEventId) {
        return Ok(await _sportEventsQueryService.GetEvent(sportEventId));
    }

    [HttpPost]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<int>> CreateEvent(
        [FromBody] SportEventCreate sportEventCreate) =>
        Ok(await _sportEventsService.CreateSportEvent(sportEventCreate));

    [HttpPut("{sportEventId:int}")]
    [Authorize(Roles = UserRole.User)]
    public async Task<ActionResult<int>> UpdateEvent(
        int sportEventId,
        [FromBody] SportEventUpdate sportEventUpdate) {
        if (sportEventId != sportEventUpdate.Id)
            return BadRequest();
        
        return Ok(await _sportEventsService.UpdateSportEvent(sportEventUpdate));
    }
        
}