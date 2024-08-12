using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CustomerAccountHandlers
{
    internal class CustomerAccountUpdateCommandHandler : IRequestHandler<CustomerAccountUpdateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CustomerAccount> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly CustomerAccountService _customerAccountService;


        public CustomerAccountUpdateCommandHandler(IMapper mapper, IValidator<CustomerAccount> validator, IUnitOfWork unitOfWork, ISessionContext sessionContext, CustomerAccountService customerAccountService)
        {
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _sessionContext = sessionContext;
            _customerAccountService = customerAccountService;
        }

        public async Task<ApiResponseDTO<object?>> Handle(CustomerAccountUpdateCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;


                var result = await ValidationHelper.ValidateAndMapAsync(
                            request,
                            _mapper,
                            _validator,
                            () => _unitOfWork.CustomerAccountRepository.GetByFilterAsync(p => p.Id == request.CustomerAccountId));

                if (result.status != 200)
                {
                    return result;
                }
          
                await _customerAccountService.UpdateCustomerAccount(result.data as CustomerAccount);

                return new ApiResponseDTO<object?>(201, _mapper.Map<CustomerAccountListDTO>(result.data), new List<string> { "Güncelleme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata işleme
                //return HandleException(ex);
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
            }

        }



        //private IDTO<object?> HandleException(Exception ex)
        //{
        //    // Exception logging veya daha ileri işlem yapılabilir
        //    return new IDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu." });
        //}
    }
}
