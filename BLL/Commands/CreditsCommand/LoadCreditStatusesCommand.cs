using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.CreditsCommand
{
    public class LoadCreditStatusesCommand : AbstrCommandWithDA<List<CreditStatus>>
    {
        public LoadCreditStatusesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<CreditStatus> Execute()
        {
            var credits = dAPoint.CreditStatusRepository.GetAll();
            return credits;
        }
    }
}
