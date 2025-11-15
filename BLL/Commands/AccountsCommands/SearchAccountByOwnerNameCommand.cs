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
            if (string.IsNullOrWhiteSpace(ownerName))
                throw new ArgumentException("Owner name cannot be empty.");

            var searchWords = ownerName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToLower())
                .ToArray();

            // Пошук рахунків, де власник відповідає введеному імені
            var accounts = dAPoint.AccountRepository.GetAll()
                .Where(acc => acc.Client != null &&
                    searchWords.All(sw =>
                        (!string.IsNullOrEmpty(acc.Client.FirstName) && acc.Client.FirstName.ToLower().Contains(sw)) ||
                        (!string.IsNullOrEmpty(acc.Client.LastName) && acc.Client.LastName.ToLower().Contains(sw)) ||
                        (!string.IsNullOrEmpty(acc.Client.MiddleName) && acc.Client.MiddleName.ToLower().Contains(sw))
                    )
                )
                .ToList();

            if (!accounts.Any())
                throw new Exception("No accounts found for this owner.");

            return accounts;
        }
    }
}
