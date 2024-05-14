using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ICountryRepository:IRepository<Country>
    {
        Task<Country> Update(Country entity);
    }
}