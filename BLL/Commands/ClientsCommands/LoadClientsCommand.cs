using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.ClientsCommands;

internal class LoadClientsCommand : AbstrCommandWithDA<List<Client>>
{
    public LoadClientsCommand(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {

    }

    public override List<Client> Execute()
    {
        var clients = dAPoint.ClientRepository.GetAll();
        if (clients == null || !clients.Any())
            throw new Exception("Accounts not found.");
        return clients;
    }
}