using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.PaymentsCommands
{
    public class AddPaymentCommand : AbstrCommandWithDA<bool>
    {
        private PaymentDto paymentDto;
        public AddPaymentCommand(PaymentDto paymentDTO, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.paymentDto = paymentDTO;
        }
        public override bool Execute()
        {
            var payment = mapper.Map<Payment>(paymentDto);
            dAPoint.PaymentRepository.Add(payment);
            dAPoint.Save();
            return true;
        }
    }
}
