using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models.Login;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class MenuOptionRepository : Repository<MenuOptions>, IMenuOptionRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuOptionRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<MenuOptions> Update(MenuOptions entity)
        {
            _db.MenuOptions.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}