using System;

namespace SportMeetingsApi.SportEvents.Events.Models;

public record SportEventInfo(
    int Id,
    string Name,
    string? Description,
    int LimitOfParticipants,
    int NumberOfFreeSpaces,
    DateTime StartDate,
    int DurationInHours,
    string Location
);