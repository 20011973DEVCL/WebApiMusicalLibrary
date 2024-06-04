using Microsoft.EntityFrameworkCore;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Models.Login;
using WebApiMusicalLibrary.Models.Sales;

namespace WebApiMusicalLibrary.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Singer> Singer { get; set; }
        public DbSet<Albun> Albun { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<MusicGenre> MusicGenre { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<MenuOptions> MenuOptions { get; set; }
        public DbSet<UserModel> UserModel {get; set;}
        public DbSet <Order> Order { get; set; }
        public DbSet <OrderDetail> OrderDetail { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
    }
}