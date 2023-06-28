using Domain.Models;
using Domain.Models.HelperModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Repositories
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public DbSet<Sound> Sounds { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SoundLikeOrDislike> SoundLikesOrDislikes { get; set; }

        public DbSet<UserSound> UserSounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSound>()
                .HasKey(us => new { us.UserId, us.SoundId });

            modelBuilder.Entity<UserSound>()
                .HasOne(us => us.User)
                .WithMany(u => u.FavoriteSounds)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserSound>()
                .HasOne(us => us.Sound)
                .WithMany(s => s.FavoriteUsers)
                .HasForeignKey(us => us.SoundId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { 
        }
      
       
    }
}