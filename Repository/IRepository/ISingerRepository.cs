using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ISingerRepository: IRepository<Singer>
    {
        Task<Singer> Update(Singer entity);
    }
}