using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class MusicGenreRepository : Repository<MusicGenre>, IMusicGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public MusicGenreRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<MusicGenre> Update(MusicGenre entity)
        {
            _db.MusicGenre.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}