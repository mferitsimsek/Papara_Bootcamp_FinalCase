using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands;
using Papara.CaptainStore.Application.CQRS.Queries.CustomerAccountQueries;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class CustomerAccountProfile : Profile
    {
        public CustomerAccountProfile()
        {
            CreateMap<CreateCustomerAccountDTO, CustomerAccount>().ReverseMap();
            CreateMap<CustomerAccountListDTO, CustomerAccount>().ReverseMap();
            CreateMap<CustomerAccountUpdateCommandRequest, CustomerAccount>().ReverseMap();
            CreateMap<CustomerAccountListQueryRequest, CustomerAccount>().ReverseMap();
            CreateMap<AccountCreatedEmailDTO, CustomerAccount>().ReverseMap();
        }
    }
}
