using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.EmployeesCommands
{
    public class AddEmployeeCommand : AbstrCommandWithDA<bool>
    {
        private EmployeeDto employeeDto;
        public AddEmployeeCommand(EmployeeDto employeeDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.employeeDto = employeeDTO;
        }
        public override bool Execute()
        {
            var employee = mapper.Map<Employee>(employeeDto);
            dAPoint.EmployeeRepository.Add(employee);
            dAPoint.Save();
            return true;
        }
    }
}
