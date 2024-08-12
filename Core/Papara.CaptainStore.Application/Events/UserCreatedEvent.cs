using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;

namespace Papara.CaptainStore.Application.Events;

public class UserCreatedEvent : INotification
{
    public Guid AppUserId { get; }
    public AppUserCreateCommandRequest Request { get; }

    public UserCreatedEvent(Guid appUserId, AppUserCreateCommandRequest request)
    {
        AppUserId = appUserId;
        Request = request;
    }
}
