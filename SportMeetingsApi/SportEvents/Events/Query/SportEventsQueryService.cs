using SportMeetingsApi.Persistence.Database;
using SportMeetingsApi.Shared.Services;

namespace SportMeetingsApi.SportEvents.Events.Query;

public class SportEventsQueryService {
    private readonly DatabaseContext _dbContext;
    private readonly IContext _context;
    public SportEventsQueryService(DatabaseContext dbContext, IContext context) {
        _dbContext = dbContext;
        _context = context;
    }
}
