using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.EmployeesCommands
{
    internal class LoadEmployeeRolesCommand : AbstrCommandWithDA<List<EmployeeRole>>
    {
        public LoadEmployeeRolesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<EmployeeRole> Execute()
        {
            var employees = dAPoint.EmployeeRoleRepository.GetAll();
            if (employees == null || !employees.Any())
                throw new Exception("Accounts not found.");
            return employees;
        }
    }
}
