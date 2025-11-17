using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.PaymentsCommands
{
    public class LoadPaymentsCommand : AbstrCommandWithDA<List<Payment>>
    {
        public LoadPaymentsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<Payment> Execute()
        {
            var payments = dAPoint.PaymentRepository.GetAll();
            if (payments == null || !payments.Any())
                throw new Exception("Accounts not found.");
            return payments;
        }
    }
}
