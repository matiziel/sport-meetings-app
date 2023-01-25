using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportMeetingsApi.Persistence;
using SportMeetingsApi.Persistence.Database;
using SportMeetingsApi.Shared.Services;

namespace SportMeetingsApi.SportEvents.SignUps.Command; 

public class SignUpsService {
    private readonly DatabaseContext _dbContext;
    private readonly IContext _context;

    public SignUpsService(DatabaseContext dbContext, IContext context) {
        _dbContext = dbContext;
        _context = context;
    }
    
    public async Task CreateSignUp(int sportEventId) {
        var isUserSignUp = await _dbContext.SignUps
            .AnyAsync(s => s.User.Id == _context.UserId && s.SportEvent.Id == sportEventId);

        if (isUserSignUp)
            return;
        
        var sportEvent = await _dbContext.SportEvents
            .SingleAsync(s =>  s.Id == sportEventId);

        var user = await _dbContext.Users
            .SingleAsync(u => u.Id == _context.UserId);

        await _dbContext.SignUps.AddAsync(new SignUp() {
            SportEvent = sportEvent,
            User = user
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSignUp(int sportEventId) {
        var signUps = await _dbContext.SignUps
            .Where(s => s.User.Id == _context.UserId && s.SportEvent.Id == sportEventId)
            .ToListAsync();
        _dbContext.SignUps.RemoveRange(signUps);
        await _dbContext.SaveChangesAsync();
    }
}