using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Genre> Update(Genre entity)
        {
            _db.Genre.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}