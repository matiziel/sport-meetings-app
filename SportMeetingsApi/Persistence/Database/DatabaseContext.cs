using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportMeetingsApi.Persistence.Database;

public class DatabaseContext : IdentityDbContext<User> {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

    }
}
