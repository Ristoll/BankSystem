using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using DTO;

namespace BLL.AutoMapperProfiles
{
    public class CreditAutoMapperProfile : Profile
    {
        public CreditAutoMapperProfile()
        {
            CreateMap<Credit, CreditDto>().ReverseMap();

            CreateMap<CreditDto, Credit>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
        }
    }
}
