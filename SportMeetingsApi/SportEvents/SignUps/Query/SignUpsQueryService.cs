using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportMeetingsApi.Persistence.Database;
using SportMeetingsApi.Shared.Services;

namespace SportMeetingsApi.SportEvents.SignUps.Query;

public class SignUpsQueryService {
    private readonly DatabaseContext _dbContext;
    private readonly IContext _context;

    public SignUpsQueryService(DatabaseContext dbContext, IContext context) {
        _dbContext = dbContext;
        _context = context;
    }

    public async Task<bool> IsUserSignUp(int sportEventId) =>
        await _dbContext.SignUps
            .AnyAsync(s => s.User.Id == _context.UserId && s.SportEvent.Id == sportEventId);
    
    public async Task<bool> CanUserSignUp(int sportEventId) {
        var sportEvent = await _dbContext.SportEvents
            .SingleAsync(s => s.Id == sportEventId);

        var numberOfSignUps = await _dbContext.SignUps
            .CountAsync(s => s.SportEvent.Id == sportEventId);

        return numberOfSignUps < sportEvent.LimitOfParticipants;
    }
}