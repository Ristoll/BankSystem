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
    public class BranchesCommandManager : AbstractCommandManager
    {
        public BranchesCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public bool AddBranch(BankBranchDto bankBranchDto)
        {
            var command = new AddBranchCommand(bankBranchDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося додати новий відділок.");
        }
        public bool UpdateBranch(BankBranchDto bankBranchDto)
        {
            var command = new UpdateBranchCommand(bankBranchDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити відділок.");
        }
        public List<BankBranch> LoadBranches()
        {
            var command = new LoadBranchesCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити відділки.");
        }
        public List<BranchType> LoadBranchTypes()
        {
            var command = new LoadBranchTypesCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити відділки.");
        }
        public bool DeleteBranch(int branchId)
        {
            var command = new DeleteBranchCommand(branchId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося видалити відділок.");
        }
    }
}
