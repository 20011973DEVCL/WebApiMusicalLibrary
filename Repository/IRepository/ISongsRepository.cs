using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ISongsRepository:IRepository<Songs>
    {
        Task<Songs> Update(Songs entity);
    }
}