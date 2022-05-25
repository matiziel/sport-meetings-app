#nullable disable

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportMeetingsApi.Persistence.Database;

public class DatabaseContext : IdentityDbContext<User> {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SportEvent>().HasKey(e => e.Id);
        modelBuilder.Entity<SportEvent>().Property(e => e.Name).IsRequired();
        modelBuilder.Entity<SportEvent>().Property(e => e.StartDate).IsRequired();
        modelBuilder.Entity<SportEvent>().Property(e => e.EndDate).IsRequired();
        modelBuilder.Entity<SportEvent>().Property(e => e.LimitOfParticipants).IsRequired();

        modelBuilder.Entity<SportEvent>()
            .HasOne(a => a.User)
            .WithMany(s => s.CreatedSportEvents)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SportEvent>()
            .HasOne(s => s.Location)
            .WithMany(l => l.SportEvents)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Location>().HasKey(l => l.Id);
        modelBuilder.Entity<Location>().Property(l => l.Name).IsRequired();

        modelBuilder.Entity<SignUp>().HasKey(s => s.Id);

        modelBuilder.Entity<SignUp>()
            .HasOne(s => s.User)
            .WithMany(u => u.SignUps)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SignUp>()
            .HasOne(s => s.SportEvent)
            .WithMany(u => u.SignUps)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<SportEvent> SportEvents { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<SignUp> SignUps { get; set; }
}
