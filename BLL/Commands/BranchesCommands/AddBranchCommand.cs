using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.BranchesCommands
{
    public class AddBranchCommand : AbstrCommandWithDA<bool>
    {
        private BankBranchDto branchDto;
        public AddBranchCommand(BankBranchDto branchDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.branchDto = branchDTO;
        }
        public override bool Execute()
        {
            var branch = mapper.Map<BankBranch>(branchDto);
            dAPoint.BankBranchRepository.Add(branch);
            dAPoint.Save();
            return true;
        }
    }
}
