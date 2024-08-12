using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Persistence.Contexts;
using System.Linq.Expressions;

namespace Papara.CaptainStore.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly MSSqlContext _context;

        public Repository(MSSqlContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(List<Expression<Func<T, object>>>? includes = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>>? includes = null)
        {
            var query = _context.Set<T>().AsQueryable().Where(filter);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>>? includes = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(filter);
        }
        public async Task<T?> GetByIdAsync(object id, params string[] includes)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    var navigationEntry = _context.Entry(entity).Member(include);

                    if (navigationEntry is CollectionEntry collectionEntry)
                    {
                        await collectionEntry.LoadAsync();
                    }
                    else if (navigationEntry is ReferenceEntry referenceEntry)
                    {
                        await referenceEntry.LoadAsync();
                    }
                }
            }
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
