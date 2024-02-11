using Microsoft.EntityFrameworkCore;

namespace MusicPortalWebApi.Models
{
    public class MusicClubContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<MusicClip> Clips { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public MusicClubContext(DbContextOptions<MusicClubContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
