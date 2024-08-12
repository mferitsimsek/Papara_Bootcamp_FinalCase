using MediatR;

namespace Papara.CaptainStore.Application.Events
{
    public class UserDeletedEvent : INotification
    {
        public Guid AppUserId { get; }

        public UserDeletedEvent(Guid appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
