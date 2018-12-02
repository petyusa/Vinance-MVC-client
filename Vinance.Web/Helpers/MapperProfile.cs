using System;
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
            CreateMissingTypeMaps = true;

            CreateMap<VinanceUserViewmodel, VinanceUser>();

            CreateMap<Income, CreateIncomeViewmodel>()
                .ForMember(i => i.AccountList, opt => { opt.Ignore(); })
                .ForMember(i => i.CategoryList, opt => { opt.Ignore(); });
            CreateMap<CreateIncomeViewmodel, Income>();

            CreateMap<Expense, CreateExpenseViewmodel>()
                .ForMember(i => i.AccountList, opt => { opt.Ignore(); })
                .ForMember(i => i.CategoryList, opt => { opt.Ignore(); });
            CreateMap<CreateExpenseViewmodel, Expense>();

            CreateMap<Transfer, CreateTransferViewmodel>()
                .ForMember(i => i.AccountList, opt => { opt.Ignore(); })
                .ForMember(i => i.CategoryList, opt => { opt.Ignore(); });
            CreateMap<CreateTransferViewmodel, Transfer>();

            CreateMap<Category, CategoryViewmodel>()
                .ForMember(dest => dest.MonthlyLimitUsed, opt =>
                {
                    opt.ResolveUsing(src =>
                    {
                        var haha = (src.Balance / (double) src.MonthlyLimit)*100;
                        var limitUsed = Math.Floor(haha);
                        return (int)limitUsed;
                    });
                });
        }
    }
}