﻿using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Services.CustomerAccountServices
{
    public interface ICustomerAccountService
    {
        Task<ApiResponseDTO<object?>> UpdateCustomerAccount(CustomerAccount customerAccount);
        Task<CustomerAccount> CreateCustomerAccountAsync(Guid userId);
        Task SendWelcomeEmailAsync(CustomerAccount customerAccount);
    }
}
