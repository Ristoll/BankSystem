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
    public class CreditsCommandManager : AbstractCommandManager
    {
        public CreditsCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public bool AddCredit(CreditDto creditDto)
        {
            var command = new AddCreditCommand(creditDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося додати новий кредит.");
        }
        public bool UpdateCredit(CreditDto creditDto)
        {
            var command = new UpdateCreditCommand(creditDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити кредит.");
        }
        public List<Credit> FilterCreditsByStatus(CreditStatusDto status)
        {
            var command = new FilterCreditsByStatusCommand(status, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося відфільтрувати кредити за статусом.");
        }
    }
}
