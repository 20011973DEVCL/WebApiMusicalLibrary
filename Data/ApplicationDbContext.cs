using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<BandSinger> BandSinger { get; set; }
        public DbSet<Albun> Albun { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Songs> Songs { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
    }
}