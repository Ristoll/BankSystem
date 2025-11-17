using AutoMapper;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Commands.AccountsCommands
{
    public class SearchAccountByOwnerNameCommand : AbstrCommandWithDA<List<Account>>
    {
        private readonly string ownerName;

        public SearchAccountByOwnerNameCommand(string ownerName, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.ownerName = ownerName;
        }

        public override List<Account> Execute()
        {
            // Розбиваємо введене ім'я на слова
            var searchWords = ownerName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToLower())
                .ToArray();

            var matchedClients = dAPoint.ClientRepository.GetAll()
                .Where(c =>
                    searchWords.All(sw =>
                        (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.ToLower().Contains(sw)) ||
                        (!string.IsNullOrEmpty(c.LastName) && c.LastName.ToLower().Contains(sw)) ||
                        (!string.IsNullOrEmpty(c.MiddleName) && c.MiddleName.ToLower().Contains(sw))
                    )
                )
                .ToList();

            if (!matchedClients.Any())
                return new List<Account>(); // Якщо клієнтів не знайдено, повертаємо порожній список

            var clientIds = matchedClients.Select(c => c.ClientId).ToList();

            var accounts = dAPoint.AccountRepository.GetAll()
                .Where(acc => clientIds.Contains(acc.ClientId))
                .ToList();

            return accounts;
        }

    }
}
