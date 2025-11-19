using System;
using System.Linq;
using Core;

namespace BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // 1. Виписка по рахунку за період
        public string GenerateAccountStatementContent(int accountId, DateTime from, DateTime to)
        {
            var account = unitOfWork.AccountRepository.GetById(accountId);
            var client = unitOfWork.ClientRepository.GetById(account.ClientId);
            var accountType = unitOfWork.AccountTypeRepository.GetById(account.AccountTypeId);
            var transactions = unitOfWork.TransactionRepository
                                .GetQueryable()
                                .Where(t => t.AccountId == accountId && t.TransactionDate >= from && t.TransactionDate <= to)
                                .ToList();

            var reportText = $"Виписка по рахунку {client.LastName} {client.FirstName} {client.MiddleName} #{account.AccountId} типу '{accountType.Name}' з {from:d} по {to:d}\r\n";

            foreach (var t in transactions)
            {
                var transactionType = unitOfWork.TransactionTypeRepository.GetById(t.TransactionTypeId);
                reportText += $"{t.TransactionDate:d} | {t.Amount} | Тип транзакції #{t.TransactionTypeId} {transactionType.Name}\r\n";
            }

            return reportText;
        }

        // 2. Список активних рахунків конкретного клієнта
        public string GenerateActiveAccountsReportContent(int clientId)
        {
            var client = unitOfWork.ClientRepository.GetById(clientId);
            var accounts = unitOfWork.AccountRepository
                            .GetQueryable()
                            .Where(a => a.ClientId == clientId && a.CloseDate == null)
                            .ToList();

            var reportText = $"Активні рахунки клієнта {client.LastName} {client.FirstName} {client.MiddleName}:\n";

            foreach (var account in accounts)
            {
                // Беремо тип рахунку з репозиторію
                var accountType = unitOfWork.AccountTypeRepository.GetById(account.AccountTypeId);
                var accountTypeName = accountType != null ? accountType.Name : "Невідомий тип";

                reportText += $"{account.AccountId} | Тип #{account.AccountTypeId} {accountTypeName} | Баланс {account.Balance}\n";
            }

            return reportText;
        }


        // 3. Сумарний кредитний портфель банку
        public string GenerateCreditPortfolioReportContent()
        {
            var credits = unitOfWork.CreditRepository.GetAll();
            decimal total = credits.Sum(c => c.CreditAmount);

            return $"Сумарний кредитний портфель банку: {total}";
        }

        // 4. Список транзакцій, сума яких перевищує вказане значення
        public string GenerateHighValueTransactionsReportContent(decimal threshold)
        {
            var transactions = unitOfWork.TransactionRepository
                                .GetQueryable()
                                .Where(t => t.Amount > threshold)
                                .ToList();

            var reportText = $"Транзакції, що перевищують {threshold}:\n";
            reportText += string.Join("\n", transactions.Select(t => $"{t.TransactionDate:d} | {t.Amount} | Рахунок {t.AccountId}"));
            return reportText;
        }

        // 5. Звіт по діяльності співробітника
        public string GenerateEmployeeActivityReportContent(int employeeId)
        {
            var employee = unitOfWork.EmployeeRepository.GetById(employeeId);
            var accountsOpened = unitOfWork.AccountRepository
                                .GetQueryable()
                                .Count(a => a.EmployeeId == employeeId);

            var creditsIssued = unitOfWork.CreditRepository
                                .GetQueryable()
                                .Count(c => c.EmployeeId == employeeId);
            var transactionsIssued = unitOfWork.TransactionRepository
                                .GetQueryable()
                                .Count(c => c.EmployeeId == employeeId);
            var reportText = $"Діяльність співробітника {employee.LastName} {employee.FirstName} {employee.MiddleName} :\r\n";
            reportText += $"Відкрито рахунків: {accountsOpened}\r\n";
            reportText += $"Оформлено кредитів: {creditsIssued}\r\n";
            reportText += $"Оформлено транзакцій: {transactionsIssued}\r\n";

            return reportText;
        }
    }
}
