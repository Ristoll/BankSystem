using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.CreditsCommands
{
    public class FilterCreditsByStatusCommand : AbstrCommandWithDA<List<Credit>>
    {
        private int statusId;
        public FilterCreditsByStatusCommand(int statusId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.statusId = statusId;
        }
        public override List<Credit> Execute()
        {
            var credits = dAPoint.CreditRepository.GetAll()
                .Where(c => c.StatusId == statusId)
                .ToList();
            return credits;
        }
    }
}
