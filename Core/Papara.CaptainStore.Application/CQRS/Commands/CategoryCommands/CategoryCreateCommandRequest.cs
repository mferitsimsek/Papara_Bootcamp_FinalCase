using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands
{
    public class CategoryCreateCommandRequest : IRequest<ApiResponseDTO<object?>>,IHasCreatedUser
    {
        public required string CategoryName { get; set; }
        public required string Url { get; set; }
        public required string Tag { get; set; }
        [JsonIgnore]
        public Guid CreatedUserId { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}
