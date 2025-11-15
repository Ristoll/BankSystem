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
        private readonly CurrencyDto currencyDto;

        public FilterAccountsByCurrencyCommand(CurrencyDto currencyDto, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.currencyDto = currencyDto;
        }
        public override List<Account> Execute()
        {
            var currency = mapper.Map<Currency>(currencyDto);
            var accounts = dAPoint.AccountRepository.GetAll()
                .Where(account => account.Currency == currency)
                .ToList();
            return accounts;
        }
    }
}

