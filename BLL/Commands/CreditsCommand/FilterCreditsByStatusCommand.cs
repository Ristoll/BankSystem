using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.CreditsCommand
{
    public class FilterCreditsByStatusCommand : AbstrCommandWithDA<List<Credit>>
    {
        private CreditStatusDto status;
        public FilterCreditsByStatusCommand(CreditStatusDto status, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.status = status;
        }
        public override List<Credit> Execute()
        {
            var credits = dAPoint.CreditRepository.GetAll()
                .Where(c => c.Status.StatusId == status.StatusId)
                .ToList();
            return credits;
        }
    }
}
