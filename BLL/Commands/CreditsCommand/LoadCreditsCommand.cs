using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.CreditsCommand;

internal class LoadCreditsCommand : AbstrCommandWithDA<List<Credit>>
{
    public LoadCreditsCommand(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {

    }

    public override List<Credit> Execute()
    {
        var credits = dAPoint.CreditRepository.GetAll();
        if (credits == null || !credits.Any())
            throw new Exception("Accounts not found.");
        return credits;
    }
}