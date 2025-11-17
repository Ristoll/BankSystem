using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.TransactionsCommands;

public class LoadTransactionTypesCommand : AbstrCommandWithDA<List<TransactionType>>
{
    public LoadTransactionTypesCommand(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {

    }

    public override List<TransactionType> Execute()
    {
        var transactions = dAPoint.TransactionTypeRepository.GetAll();
        return transactions;
    }
}
