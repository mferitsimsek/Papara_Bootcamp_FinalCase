using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Papara.CaptainStore.Application.CQRS.Queries.AppRoleQueries;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppRoleHandlers
{
    public class AppRoleListQueryHandler : IRequestHandler<AppRoleListQueryRequest, ApiResponseDTO<List<AppRoleListDTO>?>>
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public AppRoleListQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ApiResponseDTO<List<AppRoleListDTO>?>> Handle(AppRoleListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

                if (roles.Any())
                {
                    var dtoRoles = _mapper.Map<List<AppRoleListDTO>>(roles);
                    return new ApiResponseDTO<List<AppRoleListDTO>?>(200, dtoRoles, new List<string> { "Roller başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<AppRoleListDTO>?>(404, null, new List<string> { "Herhangi bir rol bulunamadı." });
            }
            catch (Exception ex)
            {
                // Hata loglama işlemleri yapılabilir
                return new ApiResponseDTO<List<AppRoleListDTO>?>(500, null, new List<string> { "Rol listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
