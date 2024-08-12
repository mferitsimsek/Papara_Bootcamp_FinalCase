using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;
using Papara.CaptainStore.Domain.Entities.LocationEntities;
using Papara.CaptainStore.Domain.Entities.OrderEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;
using Papara.CaptainStore.Persistence.Contexts;
using Papara.CaptainStore.Persistence.Repositories;

namespace Papara.CaptainStore.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MSSqlContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        public IRepository<AppUser> AppUserRepository { get; }
        public IRepository<AppRole> AppRoleRepository { get; }
        public IRepository<Product> ProductRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<CustomerAccount> CustomerAccountRepository { get; }
        public IRepository<Coupon> CouponRepository { get; }
        public IRepository<Order> OrderRepository { get; }
        public IRepository<Country> CountryRepository { get; }
        public IRepository<City> CityRepository { get; }
        public IRepository<District> DistrictRepository { get; }
        public UnitOfWork(MSSqlContext context)
        {
            _context = context;
            AppUserRepository = new Repository<AppUser>(_context);
            AppRoleRepository = new Repository<AppRole>(_context);
            ProductRepository = new Repository<Product>(_context);
            CategoryRepository = new Repository<Category>(_context);
            CustomerAccountRepository = new Repository<CustomerAccount>(_context);
            CouponRepository = new Repository<Coupon>(_context);
            OrderRepository = new Repository<Order>(_context);
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(type), _context);
                _repositories[type] = repositoryInstance;
            }

            return (IRepository<T>)_repositories[type];
        }
        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CompleteWithTransaction()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
