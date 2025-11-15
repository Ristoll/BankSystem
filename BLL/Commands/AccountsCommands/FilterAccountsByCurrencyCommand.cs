using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BLL.Commands.AccountsCommands
{
    public class FilterAccountsByCurrencyCommand : AbstrCommandWithDA<List<Account>>
    {
        private readonly string currencySymbol;

        public FilterAccountsByCurrencyCommand(string currencySymbol, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.currencySymbol = currencySymbol?.Trim().ToUpper()
                                  ?? throw new ArgumentException("Currency symbol cannot be empty.");
        }

        public override List<Account> Execute()
        {
            var accounts = dAPoint.AccountRepository.GetAll()
                .Where(a => a.Currency != null &&
                            !string.IsNullOrEmpty(a.Currency.Symbol) &&
                            a.Currency.Symbol.ToUpper() == currencySymbol)
                .ToList();

            if (!accounts.Any())
                throw new Exception($"No accounts found with currency '{currencySymbol}'.");

            return accounts;
        }
    }
}

