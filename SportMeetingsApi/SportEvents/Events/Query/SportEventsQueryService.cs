using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportMeetingsApi.Persistence;
using SportMeetingsApi.Persistence.Database;
using SportMeetingsApi.Shared.Services;
using SportMeetingsApi.SportEvents.Events.Models;
using SportMeetingsApi.Utils;

namespace SportMeetingsApi.SportEvents.Events.Query;

public class SportEventsQueryService {
    private readonly DatabaseContext _dbContext;
    private readonly IContext _context;
    public SportEventsQueryService(DatabaseContext dbContext, IContext context) {
        _dbContext = dbContext;
        _context = context;
    }

    public async Task<IEnumerable<SportEventGet>> GetEventsCreatedByUser() {
        return await _dbContext.SportEvents
            .AsNoTracking()
            .Where(e => !e.IsDeleted && e.Owner.Id == _context.UserId)
            .Select(e => new SportEventGet(e.Id, e.Name))
            .ToListAsync();
    }

    public async Task<IEnumerable<SportEventGet>> GetEvents() =>
        await _dbContext.SportEvents
            .AsNoTracking()
            .Where(e => !e.IsDeleted && e.StartDate > DateTime.Now)
            .Select(e => new SportEventGet(e.Id, e.Name))
            .ToListAsync();

    public async Task<SportEventInfo> GetEvent(int sportEventId) {
        var numberOfSignUps = await _dbContext.SignUps.CountAsync(s => s.SportEvent.Id == sportEventId);
        var sportEvent = await _dbContext.SportEvents
            .AsNoTracking()
            .SingleAsync(s => s.Id == sportEventId);

        return new SportEventInfo(
            sportEvent.Id, 
            sportEvent.Name, 
            sportEvent.Description,
            sportEvent.LimitOfParticipants, 
            sportEvent.LimitOfParticipants - numberOfSignUps, 
            sportEvent.StartDate, 
            sportEvent.DurationInHours,
            sportEvent.Location
        );
    }
    

}
