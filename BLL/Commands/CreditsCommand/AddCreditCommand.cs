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
    public class AddCreditCommand : AbstrCommandWithDA<bool>
    {
        private CreditDto creditDto;
        public AddCreditCommand(CreditDto creditDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.creditDto = creditDTO;
        }
        public override bool Execute()
        {
            var credit = mapper.Map<Credit>(creditDto);
            dAPoint.CreditRepository.Add(credit);
            dAPoint.Save();
            return true;
        }
    }
}
