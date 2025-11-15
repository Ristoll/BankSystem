using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int? EmployeeId { get; private set; }

        public void SetEmployee(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
