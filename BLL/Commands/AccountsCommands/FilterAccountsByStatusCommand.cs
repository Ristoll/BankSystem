using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BLL.Commands.AccountsCommands
{
    public class FilterAccountsByStatusCommand : AbstrCommandWithDA<List<Account>>
    {
        private readonly bool status;

        public FilterAccountsByStatusCommand(bool status, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.status = status;
        }

        public override List<Account> Execute()
        {
            List<Account> accounts;

            if (status)
            {
                accounts = dAPoint.AccountRepository.GetAll()
                    .Where(a => a.CloseDate == null)
                    .ToList();
            }
            else
            {
                accounts = dAPoint.AccountRepository.GetAll()
                    .Where(a => a.CloseDate != null)
                    .ToList();
            }

            if (!accounts.Any())
                throw new Exception("No accounts found with this status.");

            return accounts;
        }
    }
}
