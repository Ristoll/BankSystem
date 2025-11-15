using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.TransactionsCommands
{
    public class AddTransactionCommand : AbstrCommandWithDA<bool>
    {
        private TransactionDto transactionDto;
        public AddTransactionCommand(TransactionDto transactionDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.transactionDto = transactionDTO;
        }
        public override bool Execute()
        {
            var transaction = mapper.Map<Transaction>(transactionDto);
            dAPoint.TransactionRepository.Add(transaction);
            dAPoint.Save();
            return true;
        }
    }
}
