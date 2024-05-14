using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IBandSingerRepository: IRepository<BandSinger>
    {
        Task<BandSinger> Update(BandSinger entity);
    }
}