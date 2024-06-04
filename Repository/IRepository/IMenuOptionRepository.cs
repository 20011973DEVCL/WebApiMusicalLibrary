using WebApiMusicalLibrary.Models.Login;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IMenuOptionRepository:IRepository<MenuOptions>
    {
        Task<MenuOptions> Update(MenuOptions entity);
    }
}