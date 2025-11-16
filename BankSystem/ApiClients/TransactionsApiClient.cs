using BankSystem.ApiClients;
using DTO;
using System.Globalization;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class TransactionsApiClient : AbstractApiClient
{
    public TransactionsApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/transactions/add-transaction
    /// </summary>
    public async Task<bool> AddTransactionAsync(TransactionDto transactionDto)
    {
        var response = await client.PostAsJsonAsync("api/transactions/add-transaction", transactionDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// GET api/transactions/search-transaction-by-period?startDate=...&endDate=...
    /// </summary>
    public async Task<List<TransactionDto>?> SearchByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        // ISO формат гарантує коректне парсіння на бекенді
        string startStr = startDate.ToString("o", CultureInfo.InvariantCulture);
        string endStr = endDate.ToString("o", CultureInfo.InvariantCulture);

        var url = $"api/transactions/search-transaction-by-period?startDate={startStr}&endDate={endStr}";

        var response = await client.GetAsync(url);

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<TransactionDto>>();
    }
}
