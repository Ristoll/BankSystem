using System;
using System.Linq;
using Core;

namespace Services
{
    public class ReportService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // 1. Виписка по рахунку за період
        public string GenerateAccountStatementContent(int accountId, DateTime from, DateTime to)
        {
            var transactions = unitOfWork.TransactionRepository
                                .GetQueryable()
                                .Where(t => t.AccountId == accountId && t.TransactionDate >= from && t.TransactionDate <= to)
                                .ToList();

            var reportText = $"Виписка по рахунку {accountId} з {from:d} по {to:d}\n\n";
            reportText += string.Join("\n", transactions.Select(t => $"{t.TransactionDate:d} | {t.Amount} | Тип транзакції {t.TransactionTypeId}"));
            return reportText;
        }

        // 2. Список активних рахунків конкретного клієнта
        public string GenerateActiveAccountsReportContent(int clientId)
        {
            var accounts = unitOfWork.AccountRepository
                            .GetQueryable()
                            .Where(a => a.ClientId == clientId && a.CloseDate == null)
                            .ToList();

            var reportText = $"Активні рахунки клієнта {clientId}:\n";
            reportText += string.Join("\n", accounts.Select(a => $"{a.AccountId} | Тип {a.AccountTypeId} | Баланс {a.Balance}"));
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

            var reportText = $"Діяльність співробітника {employee.LastName} {employee.FirstName} {employee.MiddleName} :\n";
            reportText += $"Відкрито рахунків: {accountsOpened}\n";
            reportText += $"Оформлено кредитів: {creditsIssued}\n";

            return reportText;
        }
    }
}
