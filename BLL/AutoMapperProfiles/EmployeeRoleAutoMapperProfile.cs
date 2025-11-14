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
    public class EmployeeRoleAutoMapperProfile :Profile
    {
        public EmployeeRoleAutoMapperProfile()
        {
            CreateMap<EmployeeRole, EmployeeRoleDto>().ReverseMap();
        }
    }
}
