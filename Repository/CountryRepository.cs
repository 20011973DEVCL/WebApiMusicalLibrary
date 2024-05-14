using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;

        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            this._db=db;
        }

        public async Task<Country> Update(Country entity)
        {
            _db.Country.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}