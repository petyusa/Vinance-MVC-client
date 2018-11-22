using AutoMapper;

namespace Vinance.Web.Helpers
{
    using Contracts.Models.Domain;
    using Contracts.Models.Identity;
    using Models;
    using Models.Identity;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VinanceUserViewmodel, VinanceUser>();
            CreateMap<Income, CreateIncomeViewmodel>()
                .ForMember(i => i.AccountList, opt => { opt.Ignore(); })
                .ForMember(i => i.CategoryList, opt => { opt.Ignore(); });
        }
    }
}