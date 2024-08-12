using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Events.EventsHandlers
{
    public class CustomerCreateEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerCreateEventHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var customerAccountDto = CreateCustomerAccount(notification.AppUserId);
                var customerAccount = _mapper.Map<CustomerAccount>(customerAccountDto);

                await _unitOfWork.CustomerAccountRepository.CreateAsync(customerAccount);
                await _unitOfWork.CustomerAccountRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata işleme
            }
        }


        private CreateCustomerAccountDTO CreateCustomerAccount(Guid userId)
        {
            Random random = new Random();


            // Hesap bakiyesi ve puan bakiyesi NULL yerine 0 olarak atıyoruz.
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
