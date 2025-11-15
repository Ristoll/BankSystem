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
    public class UpdateEmployeeCommand : AbstrCommandWithDA<Employee>
    {
        private EmployeeDto employeeDto;
        public UpdateEmployeeCommand(EmployeeDto employeeDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.employeeDto = employeeDTO;
        }
        public override Employee Execute()
        {
            var employee = mapper.Map<Employee>(employeeDto);
            dAPoint.EmployeeRepository.Update(employee);
            dAPoint.Save();
            return employee;
        }
    }
}
