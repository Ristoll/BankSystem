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
    internal class LoadCurrenciesCommand : AbstrCommandWithDA<List<Currency>>
    {
        public LoadCurrenciesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<Currency> Execute()
        {
            var currencies = dAPoint.CurrencyRepository.GetAll();
            return currencies;
        }
    }
}
