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
    public class UpdateCreditCommand : AbstrCommandWithDA<Credit>
    {
        private CreditDto creditDto;
        public UpdateCreditCommand(CreditDto creditDto, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.creditDto = creditDto;
        }
        public override Credit Execute()
        {
            var credit = dAPoint.CreditRepository.GetById(creditDto.CreditId);
            if (credit == null)
            {
                throw new Exception("Credit not found");
            }
            mapper.Map(creditDto, credit);
            dAPoint.CreditRepository.Update(credit);
            dAPoint.Save();
            return credit;
        }
    }
}
