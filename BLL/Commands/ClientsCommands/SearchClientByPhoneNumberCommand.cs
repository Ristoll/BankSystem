using DTO;
using Core;
using Core.Entities;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace BLL.Commands.ClientsCommands
{
    public class SearchClientByPhoneNumberCommand : AbstrCommandWithDA<List<Client>>
    {
        private readonly string phonePart;

        public SearchClientByPhoneNumberCommand(string phonePart, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.phonePart = phonePart;
        }

        public override List<Client> Execute()
        {
            var clients = dAPoint.ClientRepository.GetAll()
                .Where(c => !string.IsNullOrEmpty(c.Phone) && c.Phone.Contains(phonePart))
                .ToList();

            return clients;
        }
    }
}
