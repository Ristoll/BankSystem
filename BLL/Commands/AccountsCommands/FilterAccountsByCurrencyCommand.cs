using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DTO;

namespace BLL.Commands.AccountsCommands
{
    public class FilterAccountsByCurrencyCommand : AbstrCommandWithDA<List<Account>>
    {
        private readonly int currencyId;

        public FilterAccountsByCurrencyCommand(int currencyId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.currencyId = currencyId;
        }
        public override List<Account> Execute()
        {
            var currency = dAPoint.CurrencyRepository.GetById(currencyId);
            var accounts = dAPoint.AccountRepository.GetAll()
                .Where(account => account.Currency == currency)
                .ToList();
            return accounts;
        }
    }
}

