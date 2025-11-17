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
    public class LoadEmployeesCommand : AbstrCommandWithDA<List<Employee>>
    {
        public LoadEmployeesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<Employee> Execute()
        {
            var employees = dAPoint.EmployeeRepository.GetAll();
            if (employees == null || !employees.Any())
                throw new Exception("Accounts not found.");
            return employees;
        }
    }
}
