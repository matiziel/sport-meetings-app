using System;

namespace SportMeetingsApi.SportEvents.Events.Models; 

public record SportEventUpdate(
    int Id, string? Description, string Location);