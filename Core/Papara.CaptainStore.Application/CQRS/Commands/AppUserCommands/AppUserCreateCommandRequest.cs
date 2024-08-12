using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands
{
    public class AppUserCreateCommandRequest : IRequest<ApiResponseDTO<object?>>,IHasCreatedUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public bool Status { get; set; } = true;
        [JsonIgnore]
        public Guid CreatedUserId { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}
