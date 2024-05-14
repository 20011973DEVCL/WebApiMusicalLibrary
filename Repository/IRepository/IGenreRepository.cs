using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IGenreRepository:IRepository<Genre>
    {
        Task<Genre> Update(Genre entity);
    }
}