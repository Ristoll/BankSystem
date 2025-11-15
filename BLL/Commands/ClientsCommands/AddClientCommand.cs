using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.ClientsCommands;

public class AddClientCommand : AbstrCommandWithDA<bool>
{
    private ClientDto clientDto;
    public AddClientCommand(ClientDto clientDTO, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.clientDto = clientDTO;
    }
    public override bool Execute()
    {
        var client = mapper.Map<Client>(clientDto);
        dAPoint.ClientRepository.Add(client);
        dAPoint.Save();
        return true;
    }
}