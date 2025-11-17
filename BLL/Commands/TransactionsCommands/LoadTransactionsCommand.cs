using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.TransactionsCommands
{
    internal class LoadTransactionsCommand : AbstrCommandWithDA<List<Transaction>>
    {
        public LoadTransactionsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<Transaction> Execute()
        {
            var transactions = dAPoint.TransactionRepository.GetAll();
            if (transactions == null || !transactions.Any())
                throw new Exception("Accounts not found.");
            return transactions;
        }
    }
}
