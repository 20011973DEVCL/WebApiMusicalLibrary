using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Create(T entity)
        {
            await this.dbSet.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entidad)
        { 
            dbSet.Remove(entidad);
            await Save();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter!=null){
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter!=null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}