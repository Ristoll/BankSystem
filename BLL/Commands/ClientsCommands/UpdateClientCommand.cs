using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using DTO;

namespace BLL.Commands.ClientsCommands;

public class UpdateClientCommand : AbstrCommandWithDA<bool>
{
    private ClientDto clientDto;
    public UpdateClientCommand(ClientDto clientDTO, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.clientDto = clientDTO;
    }
    public override bool Execute()
    {
        var client = dAPoint.AccountRepository.GetById(clientDto.ClientId);

        if (client == null)
            throw new Exception("Account not found.");

        mapper.Map(clientDto, client);

        dAPoint.AccountRepository.Update(client);
        dAPoint.Save();

        return true;
    }


}
