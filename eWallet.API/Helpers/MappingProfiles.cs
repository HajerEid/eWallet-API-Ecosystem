using AutoMapper;
using eWallet.Core.Dtos;
using eWallet.Core.Entities.eWallet;

namespace eWallet.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.TransactionType,
                            o => o.MapFrom(s => s.TransactionType.ToString()));

            CreateMap<Wallet, WalletDto>(); 

            CreateMap<Transaction, TransactionDto>() 
                .ForMember(d => d.Type,
                            o => o.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.CategoryName, 
                            o => o.MapFrom(s => s.Category.Name));
        }
    }
}
