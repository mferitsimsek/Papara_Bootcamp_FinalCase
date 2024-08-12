using AutoMapper;
using FluentValidation;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.Helpers
{
    public static class ValidationHelper
    {
        public static async Task<ApiResponseDTO<object?>> ValidateAndMapAsync<TRequest, TEntity>(
            TRequest request,
            IMapper mapper,
            IValidator<TEntity> validator,
            Func<Task<TEntity>> getEntity)
            where TEntity : class
        {
            var entity = await getEntity();
            if (entity == null)
            {
                return new ApiResponseDTO<object?>(404, null, new List<string> { "Güncellenecek veri bulunamadı." });
            }

            mapper.Map(request, entity);

            var validationResult = ValidateEntity(validator, entity);
            if (validationResult != null)
            {
                return validationResult;
            }

            return new ApiResponseDTO<object?>(200, entity, null); // Başarılı durumda 200 döner
        }

        public static async Task<ApiResponseDTO<object?>> ValidateAndMapForCreateAsync<TRequest, TEntity>(
          TRequest request,
          IMapper mapper,
          IValidator<TEntity> validator,
          Func<Task<TEntity>> getEntity)
          where TEntity : class
        {
            var entity = await getEntity();
            mapper.Map(request,entity);

            var validationResult = ValidateEntity(validator, entity);
            if (validationResult != null)
            {
                return validationResult;
            }

            return new ApiResponseDTO<object?>(200, entity, null); // Başarılı durumda 200 döner
        }

        private static ApiResponseDTO<object?>? ValidateEntity<TEntity>(IValidator<TEntity> validator, TEntity entity)
            where TEntity : class
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return new ApiResponseDTO<object?>(303, null, validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            return null;
        }
    }

}
