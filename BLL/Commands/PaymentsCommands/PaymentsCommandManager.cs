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
    public class PaymentsCommandManager : AbstractCommandManager
    {
        public PaymentsCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public bool AddPayment(PaymentDto paymentDto)
        {
            var command = new AddPaymentCommand(paymentDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося додати новий платіж.");
        }
        public List<Payment> LoadPayments()
        {
            var command = new LoadPaymentsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити платежі.");
        }
        public List<PaymentType> LoadPaymentTypes()
        {
            var command = new LoadPaymentTypesCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити платежі.");
        }
    }
}
