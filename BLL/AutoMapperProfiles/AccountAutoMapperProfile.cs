using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using DTO;

namespace BLL.AutoMapperProfiles;

public class AccountAutoMapperProfile : Profile
{
    public AccountAutoMapperProfile()
    {
        // Мапінг для відображення DTO
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.AccountTypeName, opt => opt.MapFrom(src => src.AccountType.Name))
            .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.Currency.Name))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.LastName + " " + src.Client.FirstName + " " + src.Client.MiddleName))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.LastName + " " + src.Employee.FirstName + " " + src.Employee.MiddleName : ""))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.BranchName : ""));

        // Мапінг для вставки нового Account
        CreateMap<AccountDto, Account>()
            .ForMember(dest => dest.AccountType, opt => opt.Ignore())  // ігноруємо навігаційні властивості
            .ForMember(dest => dest.Currency, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Credits, opt => opt.Ignore())
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());
    }
}

