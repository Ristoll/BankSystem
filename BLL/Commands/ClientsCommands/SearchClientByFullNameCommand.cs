using DTO;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace BLL.Commands.ClientsCommands
{
    public class SearchClientByNamePartsCommand : AbstrCommandWithDA<List<Client>>
    {
        private readonly string searchString;

        public SearchClientByNamePartsCommand(string searchString, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.searchString = searchString;
        }

        public override List<Client> Execute()
        {
            if (string.IsNullOrWhiteSpace(searchString))
                throw new ArgumentException("Search string cannot be empty.");

            // Розбиваємо введений рядок на слова
            var searchWords = searchString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToLower())
                .ToArray();

            var clients = dAPoint.ClientRepository.GetAll()
                .Where(c =>
                    searchWords.All(sw =>
                        (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.ToLower().Contains(sw)) ||
                        (!string.IsNullOrEmpty(c.LastName) && c.LastName.ToLower().Contains(sw)) ||
                        (!string.IsNullOrEmpty(c.MiddleName) && c.MiddleName.ToLower().Contains(sw))
                    )
                )
                .ToList();

            if (!clients.Any())
                throw new Exception("No clients found.");

            return clients;
        }
    }
}
