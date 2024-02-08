using Microsoft.EntityFrameworkCore;

namespace VideoKlubAPI.Models
{

    public class VideoKlubContext : DbContext
    {
        public DbSet<VideoKlub> VideoKlubovi {get; set;}
        public DbSet<Odeljenje> Odeljenja {get; set;}
        public DbSet<Film> Filmovi {get; set;}

        public VideoKlubContext(DbContextOptions options) : base(options)
        {

        }
        
        
    }
}