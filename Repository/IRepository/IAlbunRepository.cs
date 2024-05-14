using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IAlbunRepository:IRepository<Albun>
    {
        Task<Albun> Update(Albun entity);
    }
}