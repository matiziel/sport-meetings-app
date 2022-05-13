// using Microsoft.EntityFrameworkCore;

// namespace SportMeetingsApi.Persistence.Database {
//     public class ApplicationDbContext : DbContext {
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//             : base(options) { }

//         protected override void OnModelCreating(ModelBuilder modelBuilder) {
//             base.OnModelCreating(modelBuilder);

//             modelBuilder.Entity<User>().HasKey(u => u.Id);
//             modelBuilder.Entity<User>().Property(r => r.Email).IsRequired();
//             modelBuilder.Entity<User>().HasIndex(r => r.Email).IsUnique();
//             modelBuilder.Entity<User>().Property(r => r.PasswordHash).IsRequired();
//         }

//         public DbSet<User> Users { get; set; }
//     }
// }