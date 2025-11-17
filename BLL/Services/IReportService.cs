using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReportService
    {
        string GenerateAccountStatementContent(int accountId, DateTime from, DateTime to);
        string GenerateActiveAccountsReportContent(int clientId);
        string GenerateCreditPortfolioReportContent();
        string GenerateHighValueTransactionsReportContent(decimal threshold);
        string GenerateEmployeeActivityReportContent(int employeeId);
    }
}
