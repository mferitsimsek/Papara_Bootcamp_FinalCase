using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Services
{
    internal class CustomerAccountService : BaseService<CustomerAccount>
    {
        public CustomerAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<ApiResponseDTO<object?>> UpdateCustomerAccount(CustomerAccount customerAccount)
        {
            return await UpdateEntity(customerAccount);
        }

    }
}
