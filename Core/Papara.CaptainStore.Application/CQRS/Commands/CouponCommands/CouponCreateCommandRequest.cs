using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Enums;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.CouponCommands
{
    public class CouponCreateCommandRequest : IRequest<ApiResponseDTO<object?>>,IHasCreatedUser
    {
        public string? CouponCode { get; set; } // İsteğe bağlı 
        public string? CouponStartChars { get; set; } //Max 3 karakter olacak şekilde ayarlandı. Geri kalan 7 karakter otomatik oluşturulacak.
        public decimal DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int UsedCount { get; set; }
        public int MaxUsageCount { get; set; }
        [JsonIgnore]
        public Guid CreatedUserId { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}
