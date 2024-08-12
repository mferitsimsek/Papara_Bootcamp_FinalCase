using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.AppUserQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.AppUserDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserListCommandHandler : IRequestHandler<AppUserListQueryRequest, ApiResponseDTO<List<AppUserListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppUserListCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<List<AppUserListDTO>?>> Handle(AppUserListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.AppUserRepository.GetAllAsync();

                if (users.Any())
                {
                    var userList = _mapper.Map<List<AppUserListDTO>>(users);
                    return new ApiResponseDTO<List<AppUserListDTO>?>(200, userList, new List<string> { "Kullanıcılar başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<AppUserListDTO>?>(404, null, new List<string> { "Herhangi bir kullanıcı bulunamadı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<AppUserListDTO>?>(500, null, new List<string> { "Kullanıcı listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
