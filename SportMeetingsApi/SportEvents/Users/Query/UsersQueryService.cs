using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportMeetingsApi.Persistence.Database;
using SportMeetingsApi.Shared.Services;
using SportMeetingsApi.SportEvents.Users.Models;

namespace SportMeetingsApi.SportEvents.Users.Query;

public class UsersQueryService {
    private readonly DatabaseContext _dbContext;
    private readonly IContext _context;

    public UsersQueryService(DatabaseContext dbContext, IContext context) {
        _dbContext = dbContext;
        _context = context;
    }

    public async Task<UserInfo> GetUserInfo() {
        var user = await _dbContext.Users.SingleAsync(u => u.Id == _context.UserId);
        return new UserInfo(user.Id, user.UserName, user.Email);
    }
}