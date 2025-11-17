using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.AccountsCommands
{
    internal class LoadAccountTypesCommand : AbstrCommandWithDA<List<AccountType>>
    {
        public LoadAccountTypesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<AccountType> Execute()
        {
            var accountTypes = dAPoint.AccountTypeRepository.GetAll();
            if (accountTypes == null || !accountTypes.Any())
                throw new Exception("Accounts not found.");
            return accountTypes;
        }
    }
}
