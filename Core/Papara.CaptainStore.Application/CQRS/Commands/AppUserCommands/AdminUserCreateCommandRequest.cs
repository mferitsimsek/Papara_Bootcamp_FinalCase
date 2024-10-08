﻿using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands
{
    public class AdminUserCreateCommandRequest : IRequest<ApiResponseDTO<object?>>
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
    }
}
