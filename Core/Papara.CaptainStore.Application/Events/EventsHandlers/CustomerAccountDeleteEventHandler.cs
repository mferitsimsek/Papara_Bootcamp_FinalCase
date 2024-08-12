using MediatR;
using Papara.CaptainStore.Application.Interfaces;

namespace Papara.CaptainStore.Application.Events.EventHandlers
{
    public class CustomerAccountDeleteEventHandler : INotificationHandler<UserDeletedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerAccountDeleteEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                // Kullanıcının CustomerAccount kaydını bul
                var customerAccount = await _unitOfWork.CustomerAccountRepository.GetByIdAsync(notification.AppUserId);
                if (customerAccount != null)
                {
                    customerAccount.IsDeleted = true;
                    await _unitOfWork.CustomerAccountRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
            }
        }
    }
}
