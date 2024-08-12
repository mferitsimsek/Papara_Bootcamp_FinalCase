using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.Helpers
{
    public static class RequestHelper
    {
        public static ApiResponseDTO<object?> SetUserAndDate<TRequest>(TRequest request, ISessionContext sessionContext) where TRequest : class
        {
            if (!Guid.TryParse(sessionContext.Session.UserId, out var userId) || userId == Guid.Empty)
            {
                return new ApiResponseDTO<object?>(401, null, new List<string> { "Geçerli bir kullanıcı bulunamadı." });
            }

            if (userId != Guid.Empty)
            {
                if (request is IHasUpdatedUser updatedUserRequest)
                {
                    updatedUserRequest.UpdatedUserId = userId;
                    updatedUserRequest.UpdatedDate = DateTime.Now;
                }

                if (request is IHasCreatedUser createdUserRequest)
                {
                    createdUserRequest.CreatedUserId = userId;
                    createdUserRequest.CreatedDate = DateTime.Now;
                }
            }

            return null;
        }
    }


    public interface IHasUpdatedUser
    {
        Guid UpdatedUserId { get; set; }
        DateTime UpdatedDate { get; set; }
    }

    public interface IHasCreatedUser
    {
        Guid CreatedUserId { get; set; }
        DateTime CreatedDate { get; set; }
    }

}
