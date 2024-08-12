using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands
{
    public class CategoryUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>, IHasUpdatedUser
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string Url { get; set; }
        public required string Tag { get; set; }
        [JsonIgnore]
        public Guid UpdatedUserId { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }
    }
}
