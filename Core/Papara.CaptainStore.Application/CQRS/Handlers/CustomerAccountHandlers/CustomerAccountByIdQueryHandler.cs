
using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.CustomerAccountQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CustomerAccountHandlers
{
    public class CustomerAccountByIdQueryHandler : IRequestHandler<CustomerAccountByIdQueryRequest, ApiResponseDTO<CustomerAccountListDTO?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDTO<CustomerAccountListDTO?>> Handle(CustomerAccountByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerAccount = await _unitOfWork.CustomerAccountRepository.GetByIdAsync(request.CustomerAccountId);
                if (customerAccount == null)
                {
                    return new ApiResponseDTO<CustomerAccountListDTO?>(404, null, new List<string> { "Sorgulanan müşteri hesabı bulunamadı." });
                }

                var customerAccountDto = _mapper.Map<CustomerAccountListDTO>(customerAccount);
                return new ApiResponseDTO<CustomerAccountListDTO?>(200, customerAccountDto, new List<string> { "Sorgulama işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<CustomerAccountListDTO?>(500, null, new List<string> { "Sorgulama işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
