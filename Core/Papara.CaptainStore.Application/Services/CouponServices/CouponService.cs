using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Services.MailContentBuilder;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Serilog;

namespace Papara.CaptainStore.Application.Services.CouponServices
{
    public class CouponService : BaseService<Coupon>, ICouponService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailContentBuilder _emailContentBuilder;
        private readonly IMessageProducer _messageProducer;
        public CouponService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IEmailContentBuilder emailContentBuilder, IMessageProducer messageProducer) : base(unitOfWork)
        {
            _userManager = userManager;
            _emailContentBuilder = emailContentBuilder;
            _messageProducer = messageProducer;
        }

        public async Task<ApiResponseDTO<object?>?> CheckCouponIsExist(string couponCode)
        {
            return await CheckEntityExists(c => c.CouponCode == couponCode, "Eklemek istediğiniz kupon sistemde kayıtlıdır.");
        }
        public async Task<ApiResponseDTO<object?>> SaveCoupon(Coupon coupon)
        {
            return await SaveEntity(coupon);
        }

        public async Task<ApiResponseDTO<object?>> UpdateCoupon(Coupon coupon)
        {
            return await UpdateEntity(coupon);
        }
        public async Task SendCouponEmailAsync(CouponSendEmailDTO couponSendEmailDTO, CouponListDTO coupon)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(couponSendEmailDTO.UserId);
                if (user == null)
                    throw new Exception($"Kullanıcı bulunamadığı için kupon gönderilemedi. Kullanıcı ID:{couponSendEmailDTO.UserId}");


                couponSendEmailDTO.CustomerEmail = user.Email;
                couponSendEmailDTO.CustomerName = $"{user.FirstName} {user.LastName}";
                couponSendEmailDTO.CouponValidityDate = $"{coupon.ValidFrom.ToShortDateString()} - {coupon.ValidTo.ToShortDateString()}";
                couponSendEmailDTO.DiscountType = coupon.DiscountType;
                couponSendEmailDTO.DiscountAmount = coupon.DiscountAmount;
                var subject = "Yeni Kuponunuz Hazır!";
                var body = _emailContentBuilder.BuildSendCouponEmail(couponSendEmailDTO);
                var notificationTemplate = new NotificationTemplate
                {
                    Subject = subject,
                    Body = body,
                    RecipientEmail = couponSendEmailDTO.CustomerEmail
                };

                _messageProducer.ProduceMessage(notificationTemplate);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kupon e-postası gönderilirken hata oluştu.");
            }
        }
    }
}
