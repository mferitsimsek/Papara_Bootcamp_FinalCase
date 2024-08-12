using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CustomerAccountHandlers
{
    internal class CustomerAccountDeleteCommandHandler : IRequestHandler<CustomerAccountDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerAccountService _customerAccountService;

        public CustomerAccountDeleteCommandHandler(IUnitOfWork unitOfWork, CustomerAccountService customerAccountService)
        {
            _unitOfWork = unitOfWork;
            _customerAccountService = customerAccountService;
        }

        public async Task<ApiResponseDTO<object?>> Handle(CustomerAccountDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerAccount = await _unitOfWork.CustomerAccountRepository.GetByIdAsync(request.CustomerAccountId);
                if (customerAccount == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek müşteri hesabı bulunamadı." });
                }
                customerAccount.IsDeleted = true;
                await _customerAccountService.UpdateCustomerAccount(customerAccount);


                return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
