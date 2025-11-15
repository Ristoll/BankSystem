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
        private readonly string accountTypeName;

        public FilterClientsByAccountTypeCommand(string accountTypeName, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.accountTypeName = accountTypeName;
        }

        public override List<Client> Execute()
        {
            if (string.IsNullOrWhiteSpace(accountTypeName))
                throw new ArgumentException("Account type cannot be empty.");

            var clients = dAPoint.ClientRepository.GetAll()
                .Where(client => client.Accounts != null && client.Accounts.Any(a =>
                    !string.IsNullOrEmpty(a.AccountType?.Name) &&
                    a.AccountType.Name.ToLower().Contains(accountTypeName.ToLower())
                ))
                .ToList();

            if (!clients.Any())
                throw new Exception("No clients found with this account type.");

            return clients;
        }
    }
}
