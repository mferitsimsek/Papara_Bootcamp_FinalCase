using AutoMapper;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.CustomerAccountService;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerAccountService(IUnitOfWork unitOfWork, IMapper mapper, IMessageProducer messageProducer)
        {
            _mapper = mapper;
            _messageProducer = messageProducer;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<object?>> UpdateCustomerAccount(CustomerAccount customerAccount)
        {
            try
            {
                await _unitOfWork.GetRepository<CustomerAccount>().UpdateAsync(customerAccount);
                await _unitOfWork.Complete();
                return new ApiResponseDTO<object?>(201, customerAccount, new List<string> { "Güncelleme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Güncelleme işlemi sırasında bir hata oluştu.", ex.Message });
            }

        }
        public async Task<CustomerAccount> CreateCustomerAccountAsync(Guid userId)
        {
            var customerAccountDto = CreateCustomerAccountDTO(userId);
            var customerAccount = _mapper.Map<CustomerAccount>(customerAccountDto);
            await _unitOfWork.CustomerAccountRepository.CreateAsync(customerAccount);
            await _unitOfWork.Complete();
            return customerAccount;
        }

        public async Task SendWelcomeEmailAsync(CustomerAccount customerAccount)
        {
            var notificationTemplate = new NotificationTemplate
            {
                Subject = "Hoş Geldiniz - Yeni Hesabınız Oluşturuldu",
                Body = $"Sayın Müşterimiz {customerAccount.AppUser.FirstName} {customerAccount.AppUser.LastName} ,\n\nHesabınız başarıyla oluşturuldu. Hesap numaranız: {customerAccount.AccountNumber}.\n\nBizi tercih ettiğiniz için teşekkür ederiz.",
                RecipientEmail = customerAccount.AppUser.Email.ToString()
            };
            _messageProducer.ProduceMessage(notificationTemplate);
        }

        private CreateCustomerAccountDTO CreateCustomerAccountDTO(Guid userId)
        {
            Random random = new Random();
            return new CreateCustomerAccountDTO
            {
                AppUserId = userId,
                Balance = 0,
                Points = 0,
                AccountNumber = random.Next(100000, 999999),
                CreatedUserId = userId
            };
        }
    }
}
