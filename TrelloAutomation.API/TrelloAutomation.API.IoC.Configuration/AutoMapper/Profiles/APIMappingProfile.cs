using AutoMapper;
using DC = TrelloAutomation.API.API.DataContracts;
using S = TrelloAutomation.API.Services.Model;

namespace TrelloAutomation.API.IoC.Configuration.AutoMapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<DC.User, S.User>().ReverseMap();
            CreateMap<DC.Address, S.Address>().ReverseMap();
        }
    }
}
