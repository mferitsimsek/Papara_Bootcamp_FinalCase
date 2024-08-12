using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands
{
    public class CustomerAccountUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int CustomerAccountId { get; set; }
        public int AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Points { get; set; }
        public Guid? AppUserId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
