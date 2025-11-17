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
    public class LoadAccountsCommand : AbstrCommandWithDA<List<Account>>
    {
        public LoadAccountsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<Account> Execute()
        {
            var accounts = dAPoint.AccountRepository.GetAll();
            if (accounts == null || !accounts.Any())
                throw new Exception("Accounts not found.");
            return accounts;
        }
    }
}
