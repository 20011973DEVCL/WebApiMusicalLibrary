using WebApiMusicalLibrary.Models.Sales;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        Task<OrderDetail> Update(OrderDetail entity);
    }
}