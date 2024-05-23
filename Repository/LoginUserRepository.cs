using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class LoginUserRepository : Repository<UserModel>, ILoginUserRepository
    {
        private readonly ApplicationDbContext _db;

        public LoginUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<UserModel> Update(UserModel entity)
        {
            _db.UserModel.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}