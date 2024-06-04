using WebApiMusicalLibrary.Models.Sales;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IOrderRepository:IRepository<Order>
    {
        Task<Order> Update(Order entity);
    }
}