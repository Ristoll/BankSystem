using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.BranchesCommands;
public class DeleteBranchCommand : AbstrCommandWithDA<bool>
{
    private int branchId;
    public DeleteBranchCommand(int branchId, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.branchId = branchId;
    }
    public override bool Execute()
    {
        dAPoint.BankBranchRepository.Remove(branchId);
        dAPoint.Save();
        return true;
    }
}