using AutoMapper;
using Core;
using DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.EmployeesCommands
{
    public class EmployeesCommandManager : AbstractCommandManager
    {
        IPasswordHasher passwordHasher;
        ICurrentUserService currentUserService;
        public EmployeesCommandManager(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, ICurrentUserService currentUserService)
            : base(unitOfWork, mapper) 
        {
            this.passwordHasher = passwordHasher;
            this.currentUserService = currentUserService;
        }
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
        public bool LoginEmployee(string phone, string password)
        {
            var command = new LogInCommand(phone, password, unitOfWork, mapper, passwordHasher, currentUserService);
            return ExecuteCommand(command, "Не вдалося увійти до системи.");
        }
    }
}
