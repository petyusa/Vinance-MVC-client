using AutoMapper;
using Vinance.Contracts.Models.Identity;
using Vinance.Web.Models.Identity;

namespace Vinance.Web.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VinanceUserViewmodel, VinanceUser>();
        }
    }
}