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
            if (string.IsNullOrWhiteSpace(phonePart) || phonePart.Length < 4)
                throw new ArgumentException("Enter at least 4 digits for search.");

            var clients = dAPoint.ClientRepository.GetAll()
                .Where(c => !string.IsNullOrEmpty(c.Phone) && c.Phone.Contains(phonePart))
                .ToList();

            if (!clients.Any())
                throw new Exception("No clients found.");

            return clients;
        }
    }
}
