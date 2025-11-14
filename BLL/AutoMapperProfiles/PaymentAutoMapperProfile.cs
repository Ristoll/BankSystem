using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using DTO;

namespace BLL.AutoMapperProfiles;

public class PaymentAutoMapperProfile : Profile
{
    public PaymentAutoMapperProfile()
    {
        CreateMap<Payment, PaymentDto>().ReverseMap();
    }
}
