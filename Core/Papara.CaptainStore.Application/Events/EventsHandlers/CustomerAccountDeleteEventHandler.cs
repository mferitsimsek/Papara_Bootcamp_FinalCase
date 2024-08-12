using MediatR;
using Papara.CaptainStore.Application.Interfaces;
using Serilog;

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
                    await _unitOfWork.CustomerAccountRepository.DeleteAsync(customerAccount);
                    await _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Kullanıcı silinirken CustomerAccount silme işlemi sırasında hata oluştu. AppUserId: {AppUserId}", notification.AppUserId);
            }
        }
    }
}
