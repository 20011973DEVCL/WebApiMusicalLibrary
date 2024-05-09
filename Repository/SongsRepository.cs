using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class SongsRepository : Repository<Songs>, ISongsRepository
    {
        private readonly ApplicationDbContext _db;

        public SongsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Songs> Update(Songs entity)
        {
            _db.Songs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}