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
    public class SearchTransactionByPeriodCommand : AbstrCommandWithDA<List<Transaction>>
    {
        private DateTime startDate;
        private DateTime endDate;
        public List<TransactionDto> Result { get; private set; }
        public SearchTransactionByPeriodCommand(DateTime startDate, DateTime endDate, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            Result = new List<TransactionDto>();
        }
        public override List<Transaction> Execute()
        {
            var transactions = dAPoint.TransactionRepository.GetAll()
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .ToList();
            return transactions;
        }
    }
}
