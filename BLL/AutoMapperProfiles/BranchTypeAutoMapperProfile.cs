using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using DTO;

namespace BLL.AutoMapperProfiles
{
    public class BranchTypeAutoMapperProfile : Profile
    {
        public BranchTypeAutoMapperProfile()
        {
            CreateMap<BranchType, BranchTypeDto>().ReverseMap();
        }
    }
}
