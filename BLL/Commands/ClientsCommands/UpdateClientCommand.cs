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
        try
        {
            var client = dAPoint.ClientRepository.GetById(clientDto.ClientId);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            client.FirstName = clientDto.FirstName;
            client.LastName = clientDto.LastName;
            client.MiddleName = clientDto.MiddleName;
            client.DateOfBirth = clientDto.DateOfBirth;
            client.PassportNumber = clientDto.PassportNumber;
            client.Email = clientDto.Email;
            client.Phone = clientDto.Phone;
            client.Address = clientDto.Address;
            dAPoint.ClientRepository.Update(client);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating client: " + ex.Message);
        }
    }


}
