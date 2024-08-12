using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.PaymentCommands
{
    public class PaymentCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int OrderId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        [JsonIgnore]
        public decimal Amount { get; set; }
    }
}
