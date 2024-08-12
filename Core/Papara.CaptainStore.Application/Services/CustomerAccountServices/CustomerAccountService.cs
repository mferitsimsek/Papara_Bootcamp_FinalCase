using AutoMapper;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Services.MailContentBuilder;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Services.CustomerAccountServices
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailContentBuilder _emailContentBuilder;

        public CustomerAccountService(IUnitOfWork unitOfWork, IMapper mapper, IMessageProducer messageProducer, IEmailContentBuilder emailContentBuilder)
        {
            _mapper = mapper;
            _messageProducer = messageProducer;
            _unitOfWork = unitOfWork;
            _emailContentBuilder = emailContentBuilder;
        }

        public async Task<ApiResponseDTO<object?>> UpdateCustomerAccount(CustomerAccount customerAccount)
        {
            try
            {
                await _unitOfWork.CustomerAccountRepository.UpdateAsync(customerAccount);
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
                Body = _emailContentBuilder.BuildAccountCreatedEmail(_mapper.Map<AccountCreatedEmailDTO>(customerAccount)),
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
                CreatedUserId = userId,
                CreatedDate = DateTime.Now
            };
        }
    }
}
