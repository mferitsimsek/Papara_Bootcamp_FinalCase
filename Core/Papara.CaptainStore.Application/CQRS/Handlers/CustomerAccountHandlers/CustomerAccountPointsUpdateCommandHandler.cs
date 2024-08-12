﻿using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;

public class CustomerAccountPointsUpdateCommandHandler : IRequestHandler<CustomerAccountPointsUpdateCommandRequest, ApiResponseDTO<object?>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerAccountPointsUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponseDTO<object?>> Handle(CustomerAccountPointsUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var customerAccount = await _unitOfWork.CustomerAccountRepository.GetByFilterAsync(ca => ca.Id == request.CustomerAccountId);
            if (customerAccount == null)
            {
                return new ApiResponseDTO<object?>(404, null, new List<string> { "Müşteri hesabı bulunamadı." });
            }

            customerAccount.Points += request.Points; // Puan güncelleme işlemi
            await _unitOfWork.CustomerAccountRepository.UpdateAsync(customerAccount);
            await _unitOfWork.Complete();

            return new ApiResponseDTO<object?>(200, _mapper.Map<CustomerAccountListDTO>(customerAccount), new List<string> { "Puan güncelleme işlemi başarılı." });
        }
        catch (Exception ex)
        {
            return new ApiResponseDTO<object?>(500, null, new List<string> { "Puan güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
        }
    }
}
