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
    internal class LoadPaymentTypesCommand : AbstrCommandWithDA<List<PaymentType>>
    {
        public LoadPaymentTypesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {

        }

        public override List<PaymentType> Execute()
        {
            var payments = dAPoint.PaymentTypeRepository.GetAll();
            return payments;
        }
    }
}
