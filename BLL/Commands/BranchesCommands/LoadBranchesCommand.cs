using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.BranchesCommands
{
    public class LoadBranchesCommand : AbstrCommandWithDA<List<BankBranch>>
    {
        public LoadBranchesCommand(IUnitOfWork operateUnitOfWork, IMapper mapper) 
            : base(operateUnitOfWork, mapper)
        {
        }

        public override List<BankBranch> Execute()
        {
            var branches = dAPoint.BankBranchRepository.GetAll();
            if (branches == null || !branches.Any())
                throw new Exception("Accounts not found.");
            return branches;
        }
    }
}
