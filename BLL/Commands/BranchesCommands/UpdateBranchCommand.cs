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
    public class UpdateBranchCommand : AbstrCommandWithDA<BankBranch>
    {
        private BankBranchDto branchDto;
        public UpdateBranchCommand(BankBranchDto branchDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.branchDto = branchDTO;
        }
        public override BankBranch Execute()
        {
            var branch = dAPoint.BankBranchRepository.GetById(branchDto.BranchId);
            if (branch == null)
            {
                throw new Exception("Branch not found");
            }
            mapper.Map(branchDto, branch);
            dAPoint.BankBranchRepository.Update(branch);
            dAPoint.Save();
            return branch;
        }
    }
}
