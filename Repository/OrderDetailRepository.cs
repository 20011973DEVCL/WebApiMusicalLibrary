using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models.Sales;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<OrderDetail> Update(OrderDetail entity)
        {
            _db.OrderDetail.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}