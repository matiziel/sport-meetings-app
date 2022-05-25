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

    public async Task<IEnumerable<SportEventGet>> GetEventsCreatedByUser(Paging paging) {
        return await _dbContext.SportEvents
            .AsNoTracking()
            .Where(e => !e.IsDeleted && e.User.Id == _context.UserId)
            .Skip(paging.Skip)
            .Take(paging.Take)
            .Select(e => new SportEventGet(e.Id, e.Name))
            .ToListAsync();
    }

    public async Task<IEnumerable<SportEventGet>> GetEvents(Paging paging) {
        return await GetActualEvents()
            .Skip(paging.Skip)
            .Take(paging.Take)
            .Select(e => new SportEventGet(e.Id, e.Name))
            .ToListAsync();
    }

    private IQueryable<SportEvent> GetActualEvents() {
        return _dbContext.SportEvents
            .AsNoTracking()
            .Where(e => !e.IsDeleted && e.EndDate > DateTime.Now);
    }

}
