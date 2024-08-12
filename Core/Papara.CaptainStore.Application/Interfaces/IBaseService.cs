using Papara.CaptainStore.Domain.DTOs;
using System.Linq.Expressions;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponseDTO<object?>?> CheckEntityExists(Expression<Func<T, bool>> predicate, string errorMessage);
        Task<ApiResponseDTO<object?>> SaveEntity(T entity);
    }
}

