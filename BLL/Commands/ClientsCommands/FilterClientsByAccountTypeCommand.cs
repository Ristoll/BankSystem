using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DTO;

namespace BLL.Commands.ClientsCommands
{
    public class FilterClientsByAccountTypeCommand : AbstrCommandWithDA<List<ClientDto>>
    {
        private readonly int accountTypeId;

        public FilterClientsByAccountTypeCommand(int accountTypeId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.accountTypeId = accountTypeId;
        }

        public override List<ClientDto> Execute()
        {
            var accounts = dAPoint.AccountRepository.GetQueryable()
                .Where(a => a.AccountTypeId == accountTypeId)
                .ToList();

            var clientIds = accounts.Select(a => a.ClientId).Distinct().ToList();

            var clients = dAPoint.ClientRepository.GetQueryable()
                .Where(c => clientIds.Contains(c.ClientId))
                .ToList();

            return mapper.Map<List<ClientDto>>(clients);
        }


    }
}
