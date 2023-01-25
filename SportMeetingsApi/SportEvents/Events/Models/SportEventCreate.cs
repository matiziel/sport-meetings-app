using System;
using SportMeetingsApi.Persistence;

namespace SportMeetingsApi.SportEvents.Events.Models; 

public record SportEventCreate(
    string Name, string? Description, int LimitOfParticipants, DateTime StartDate, int DurationInHours, string Location
);