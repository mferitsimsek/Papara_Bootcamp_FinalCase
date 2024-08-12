using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;
using Papara.CaptainStore.Domain.Entities.LocationEntities;
using Papara.CaptainStore.Domain.Entities.OrderEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task Complete();
        Task CompleteWithTransaction();
        IRepository<T> GetRepository<T>() where T : class;
        IRepository<AppUser> AppUserRepository { get; }
        IRepository<AppRole> AppRoleRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<CustomerAccount> CustomerAccountRepository { get; }
        IRepository<Coupon> CouponRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Country> CountryRepository { get; }
        IRepository<City> CityRepository { get; }
        IRepository<District> DistrictRepository { get; }
    }
}
