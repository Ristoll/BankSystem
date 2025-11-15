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
    public class DeleteEmployeeCommand : AbstrCommandWithDA<bool>
    {
        private int employeeId;
        public DeleteEmployeeCommand(int employeeId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.employeeId = employeeId;
        }
        public override bool Execute()
        {
            dAPoint.EmployeeRepository.Remove(employeeId);
            dAPoint.Save();
            return true;
        }
    }
}
