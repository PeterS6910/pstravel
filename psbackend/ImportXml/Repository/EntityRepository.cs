using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class EntityRepository<T,TId> where T : BaseEntity<TId>
    {
        protected readonly DbContext _context;

        public EntityRepository(DbContext context)
        {
            _context = context;
        }

        public async Task UpsertAsync(T entity)
        {
            var existing = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(entity.Id));
            if (existing == null)
                await _context.Set<T>().AddAsync(entity);
            else
                _context.Entry(existing).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                var existing = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(entity.Id));
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Entity with ID {entity.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("entity:" + entity.ToString());
            }
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => EqualityComparer<TId>.Default.Equals(e.Id, id));
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }
    }

}
