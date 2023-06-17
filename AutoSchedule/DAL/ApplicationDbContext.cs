using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Enum;
using AutoSchedule.Models.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Load> Loads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Email = "Admin@gmail.com",
                        Password = HashPasswordHelper.HashPassowrd("123456"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "Moderator@gmail.com",
                        Password = HashPasswordHelper.HashPassowrd("654321"),
                        Role = Role.Moderator
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.UserId);
            });

            modelBuilder.Entity<Teacher>(builder =>
            {
                builder.ToTable("Teachers").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(r => r.User).WithMany(t => t.Teachers)
                    .HasForeignKey(x => x.UserId);
            });
            
            modelBuilder.Entity<Audience>(builder =>
            {
                builder.ToTable("Audiences").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(r => r.User).WithMany(t => t.Audiences)
                    .HasForeignKey(x => x.UserId);
            });   

            modelBuilder.Entity<Load>(builder =>
            {
                builder.ToTable("Loads").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(r => r.User).WithMany(t => t.Loads)
                    .HasForeignKey(x => x.UserId);
            });
        }
    }
}
