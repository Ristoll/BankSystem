using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.ClientsCommands
{
    public class ClientsCommandManager : AbstractCommandManager
    {
        public ClientsCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public bool AddClient(ClientDto clientDto)
        {
            var command = new AddClientCommand(clientDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося додати нового клієнта.");
        }
        public bool UpdateClient(ClientDto clientDto)
        {
            var command = new UpdateClientCommand(clientDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити інформацію про клієнта.");
        }
        public List<Client> LoadClients()
        {
            var command = new LoadClientsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити дані про клієнтів.");
        }
        public List<Client> SearchClientByFullName(string fullName)
        {
            var command = new SearchClientByNamePartsCommand(fullName, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти клієнтів за вказаним ПІБ.");
        }
        public List<Client> SearchClientByPhoneNumber(string phonePart)
        {
            var command = new SearchClientByPhoneNumberCommand(phonePart, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти клієнтів за вказаним номером телефону.");
        }
        public List<ClientDto> FilterClientsByAccountType(int accountTypeId)
        {
            var command = new FilterClientsByAccountTypeCommand(accountTypeId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося відфільтрувати клієнтів за типом рахунку.");
        }



    }
}
