﻿using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands
{
    public class CustomerAccountDepositCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int CustomerAccountId { get; set; }
        public decimal DepositAmount { get; set; }
    }
}
