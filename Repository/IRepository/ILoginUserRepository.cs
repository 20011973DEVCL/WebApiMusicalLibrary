using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ILoginUserRepository:IRepository<UserModel>
    {
        Task<UserModel> Update(UserModel entity);
    }
}