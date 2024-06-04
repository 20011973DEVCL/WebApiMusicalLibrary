using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models.Sales;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Order> Update(Order entity)
        {
            _db.Order.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}