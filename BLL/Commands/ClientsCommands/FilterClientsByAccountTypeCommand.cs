using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DTO;

namespace BLL.Commands.ClientsCommands
{
    public class FilterClientsByAccountTypeCommand : AbstrCommandWithDA<List<Client>>
    {
        private readonly AccountTypeDto accountTypeDto;

        public FilterClientsByAccountTypeCommand(AccountTypeDto accountTypeDto, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.accountTypeDto = accountTypeDto;
        }

        public override List<Client> Execute()
        {
            var accountType = mapper.Map<AccountType>(accountTypeDto);
            var clients = dAPoint.ClientRepository.GetAll()
                .Where(c => c.Accounts.Any(a => a.AccountType == accountType))
                .ToList();

            if (!clients.Any())
                throw new Exception("No clients found with this account type.");

            return clients;
        }
    }
}
