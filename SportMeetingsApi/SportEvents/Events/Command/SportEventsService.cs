using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportMeetingsApi.Persistence;
using SportMeetingsApi.Persistence.Database;
using SportMeetingsApi.Shared.Services;
using SportMeetingsApi.SportEvents.Events.Models;

namespace SportMeetingsApi.SportEvents.Events.Command; 

public class SportEventsService {
    private readonly DatabaseContext _dbContext;
    private readonly IContext _context;
    public SportEventsService(DatabaseContext dbContext, IContext context) {
        _dbContext = dbContext;
        _context = context;
    }

    public async Task<int> CreateSportEvent(SportEventCreate sportEventCreate) {
        var user = await _dbContext.Users.SingleAsync(u => u.Id == _context.UserId);
        var sportEvent = new SportEvent() {
            Owner = user,
            Name = sportEventCreate.Name,
            Description = sportEventCreate.Description,
            Location = sportEventCreate.Location,
            LimitOfParticipants = sportEventCreate.LimitOfParticipants,
            StartDate = sportEventCreate.StartDate,
            DurationInHours = sportEventCreate.DurationInHours,
            IsDeleted = false
        };
        await _dbContext.SportEvents.AddAsync(sportEvent);
        await _dbContext.SaveChangesAsync();

        return sportEvent.Id;
    }

    public async Task<int> UpdateSportEvent(SportEventUpdate sportEventUpdate) {
        var sportEvent = await _dbContext.SportEvents.SingleAsync(s => s.Id == sportEventUpdate.Id);

        sportEvent.Description = sportEventUpdate.Description;
        sportEvent.Location = sportEventUpdate.Location;
        
        _dbContext.SportEvents.Update(sportEvent);
        await _dbContext.SaveChangesAsync();

        return sportEvent.Id;
    }
}