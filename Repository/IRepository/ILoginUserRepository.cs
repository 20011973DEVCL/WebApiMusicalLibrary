using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ILoginUserRepository:IRepository<User>
    {
        Task<User> Update(User entity);
    }
}