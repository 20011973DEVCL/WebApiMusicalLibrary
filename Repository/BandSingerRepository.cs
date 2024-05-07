using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class BandSingerRepository : Repository<BandSinger>, IBandSingerRepository
    {
        private readonly ApplicationDbContext _db;

        public BandSingerRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<BandSinger> Update(BandSinger entity)
        {
            _db.BandSinger.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}