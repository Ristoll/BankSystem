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
    public class TransactionCommandManager : AbstractCommandManager
    {
        public TransactionCommandManager(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper) { }

        public bool AddTransaction(TransactionDto transactionDto)
        {
            var command = new AddTransactionCommand(transactionDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вділося додати транзакцію.");
        }

        public List<Transaction> SearchTransactionByPerioud(DateTime startDate, DateTime endDate)
        {
            var command = new SearchTransactionByPeriodCommand(startDate, endDate, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти транзакції за вказаний період.");
        }
    }
}
