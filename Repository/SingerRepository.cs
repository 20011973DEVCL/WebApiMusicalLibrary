using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class SingerRepository : Repository<Singer>, ISingerRepository
    {
        private readonly ApplicationDbContext _db;

        public SingerRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Singer> Update(Singer entity)
        {
            // _db.BandSinger.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}