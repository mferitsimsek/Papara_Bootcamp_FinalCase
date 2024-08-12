using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands
{
    public class CustomerAccountUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>, IHasUpdatedUser
    {
        public int CustomerAccountId { get; set; }
        public int AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Points { get; set; }
        public Guid? AppUserId { get; set; }
        public bool? IsDeleted { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }
        [JsonIgnore]
        public Guid UpdatedUserId { get; set; }
    }
}
