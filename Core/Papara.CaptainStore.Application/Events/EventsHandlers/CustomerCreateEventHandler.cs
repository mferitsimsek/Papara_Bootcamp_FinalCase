using MediatR;
using Papara.CaptainStore.Application.Services.CustomerAccountServices;
using Serilog;

namespace Papara.CaptainStore.Application.Events.EventsHandlers
{
    public class CustomerCreateEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ICustomerAccountService _customerAccountService;
        public CustomerCreateEventHandler(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
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
