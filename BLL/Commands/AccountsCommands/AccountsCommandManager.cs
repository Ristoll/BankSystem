using AutoMapper;
using Core;
using Core.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands.AccountsCommands
{
    public class AccountsCommandManager : AbstractCommandManager
    {
        public AccountsCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public bool AddAccount(AccountDto accountDto)
        {
            var command = new AddAccountCommand(accountDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося додати новий акаунт.");
        }

        public bool UpdateAccount(AccountDto accountDto)
        {
            var command = new UpdateAccountCommand(accountDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити акаунт.");
        }

        public List<Account> LoadAccounts()
        {
            var command = new LoadAccountsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити акаунти.");
        }
        public List<AccountType> LoadAccountTypes()
        {
            var command = new LoadAccountTypesCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити типи акаунтів.");
        }
        public List<Currency> LoadCurrencies()
        {
            var command = new LoadCurrenciesCommand(unitOfWork,mapper);
            return ExecuteCommand(command, "Не вдалося завантажити валюти.");
        }
        public List<Account> FilterAccountsByCurrency(int currencyId)
        {
            var command = new FilterAccountsByCurrencyCommand(currencyId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося відфільтрувати акаунти");
        }

        public List<Account> FilterAccountsByStatus(bool status)
        {
            var command = new FilterAccountsByStatusCommand(status, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося відфільтрувати акаунти");
        }

        public List<Account> SearchAccountByOwnerName(string ownerName)
        {
            var command = new SearchAccountByOwnerNameCommand(ownerName, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти акаунти за ім'ям власника.");
        }
    }
}
