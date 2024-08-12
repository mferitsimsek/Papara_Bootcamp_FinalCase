using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using System.Linq.Expressions;

namespace Papara.CaptainStore.Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<object?>?> CheckEntityExists(Expression<Func<T, bool>> predicate, string errorMessage)
        {
            var entityExist = await _unitOfWork.GetRepository<T>().GetByFilterAsync(predicate);
            if (entityExist != null)
            {
                return new ApiResponseDTO<object?>(303, null, new List<string> { errorMessage });
            }
            return null;
        }

        public async Task<ApiResponseDTO<object?>> SaveEntity(T entity)
        {
            try
            {
                await _unitOfWork.GetRepository<T>().CreateAsync(entity);
                await _unitOfWork.Complete();
                return new ApiResponseDTO<object?>(201, entity, new List<string> { "Kayıt işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir hata oluştu.", ex.Message });
            }


        }
        public async Task<ApiResponseDTO<object?>> UpdateEntity(T entity)
        {
            try
            {
                await _unitOfWork.GetRepository<T>().UpdateAsync(entity);
                await _unitOfWork.Complete();
                return new ApiResponseDTO<object?>(201, entity, new List<string> { "Güncelleme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Güncelleme işlemi sırasında bir hata oluştu.", ex.Message });
            }
        }

    }
}
