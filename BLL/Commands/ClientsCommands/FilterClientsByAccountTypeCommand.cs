using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BLL.Commands.ClientsCommands
{
    public class FilterClientsByAccountTypeCommand : AbstrCommandWithDA<List<Client>>
    {
        private readonly AccountType accountType;

        public FilterClientsByAccountTypeCommand(AccountType accountType, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.accountType = accountType
                ?? throw new ArgumentException("AccountType cannot be null.");
        }

        public override List<Client> Execute()
        {
            var clients = dAPoint.ClientRepository.GetAll()
                .Where(client => client.Accounts != null &&
                                 client.Accounts.Any(acc =>
                                     acc.AccountType != null &&
                                     acc.AccountType.AccountTypeId == accountType.AccountTypeId
                                 ))
                .ToList();

            if (!clients.Any())
                throw new Exception("No clients found with this account type.");

            return clients;
        }
    }
}
