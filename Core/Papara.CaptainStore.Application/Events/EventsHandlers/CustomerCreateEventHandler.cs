using MediatR;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.CustomerAccountService;
using Serilog;

namespace Papara.CaptainStore.Application.Events.EventsHandlers
{
    public class CustomerCreateEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerCreateEventHandler(ICustomerAccountService customerAccountService, IUnitOfWork unitOfWork)
        {
            _customerAccountService = customerAccountService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var customerAccount = await _customerAccountService.CreateCustomerAccountAsync(notification.AppUserId);
                await _customerAccountService.SendWelcomeEmailAsync(customerAccount);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kullanıcı oluşturulurken CustomerAccount oluşturma işlemi sırasında hata oluştu. AppUserId: {AppUserId}", notification.AppUserId);
            }
        }
    }
}
