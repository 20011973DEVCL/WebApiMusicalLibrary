using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class LoginUserRepository : Repository<User>, ILoginUserRepository
    {
        private readonly ApplicationDbContext _db;

        public LoginUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<User> Update(User entity)
        {
            _db.User.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}