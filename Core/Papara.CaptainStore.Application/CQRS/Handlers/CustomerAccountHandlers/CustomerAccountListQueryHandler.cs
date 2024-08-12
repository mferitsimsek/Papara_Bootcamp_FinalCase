using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.CustomerAccountQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CustomerAccountHandlers
{
    public class CustomerAccountListQueryHandler : IRequestHandler<CustomerAccountListQueryRequest, ApiResponseDTO<List<CustomerAccountListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerAccountListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<List<CustomerAccountListDTO>?>> Handle(CustomerAccountListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerAccounts = await _unitOfWork.CustomerAccountRepository.GetAllAsync();

                if (customerAccounts.Any())
                {
                    var customerAccountsDto = _mapper.Map<List<CustomerAccountListDTO>>(customerAccounts);
                    return new ApiResponseDTO<List<CustomerAccountListDTO>?>(200, customerAccountsDto, new List<string> { "Müşteri hesapları başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<CustomerAccountListDTO>?>(404, null, new List<string> { "Herhangi bir müşteri hesabı bulunamadı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<CustomerAccountListDTO>?>(500, null, new List<string> { "Müşteri hesap listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
