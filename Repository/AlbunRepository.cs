using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class AlbunRepository : Repository<Albun>, IAlbunRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbunRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Albun> Update(Albun entity)
        {
            _db.Albun.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}