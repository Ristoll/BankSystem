using AutoMapper;
using Core;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.EmployeesCommands
{
    public class EmployeesCommandManager : AbstractCommandManager
    {
        public EmployeesCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }
        public bool AddEmployee(EmployeeDto employeeDto)
        {
            var command = new AddEmployeeCommand(employeeDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося додати нового працівника.");
        }
        public bool UpdateEmployee(EmployeeDto employeeDto)
        {
            var command = new UpdateEmployeeCommand(employeeDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити інформацію про працівника.");
        }
        public bool DeleteEmployee(int employeeId)
        {
            var command = new DeleteEmployeeCommand(employeeId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося видалити працівника.");
        }
    }
}
