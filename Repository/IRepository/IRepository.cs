using System.Linq.Expressions;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        Task Create(T entidad);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter=null);
        Task<T> GetOne(Expression<Func<T, bool>>? filkter=null, bool tracked=true);
        Task Delete(T entidad);
        Task Save(); 
    }
}