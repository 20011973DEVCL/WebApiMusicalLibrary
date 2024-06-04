using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ILoginUserRepository:IRepository<UserModel>
    {
        Task<UserModel> Update(UserModel entity);
    }
}