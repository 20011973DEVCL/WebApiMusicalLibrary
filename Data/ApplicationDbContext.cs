using Microsoft.EntityFrameworkCore;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Models.Login;

namespace WebApiMusicalLibrary.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<BandSinger> BandSinger { get; set; }
        public DbSet<Albun> Albun { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<MenuOptions> MenuOptions { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
    }
}