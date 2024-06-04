using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IMusicGenreRepository:IRepository<MusicGenre>
    {
        Task<MusicGenre> Update(MusicGenre entity);
    }
}