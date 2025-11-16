using DTO;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class AccountsApiClient : AbstractApiClient
{
    public AccountsApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/accounts/add-account
    /// </summary>
    public async Task<bool> AddAccountAsync(AccountDto account)
    {
        var response = await client.PostAsJsonAsync("api/accounts/add-account", account);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// PUT api/accounts/update-account
    /// </summary>
    public async Task<bool> UpdateAccountAsync(AccountDto account)
    {
        var response = await client.PutAsJsonAsync("api/accounts/update-account", account);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// GET api/accounts/filter-account-by-currency
    /// </summary>
    public async Task<List<AccountDto>?> FilterByCurrencyAsync(CurrencyDto dto)
    {
        var response = await client.GetAsync(
            $"api/accounts/filter-account-by-currency?CurrencyName={dto.Name}");

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<AccountDto>>();
    }

    /// <summary>
    /// GET api/accounts/filter-account-by-status?status=true
    /// </summary>
    public async Task<List<AccountDto>?> FilterByStatusAsync(bool status)
    {
        var response = await client.GetAsync(
            $"api/accounts/filter-account-by-status?status={status}");

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<AccountDto>>();
    }

    /// <summary>
    /// GET api/accounts/search-account-by-owner?ownerName=...
    /// </summary>
    public async Task<List<AccountDto>?> SearchByOwnerAsync(string ownerName)
    {
        var response = await client.GetAsync(
            $"api/accounts/search-account-by-owner?ownerName={ownerName}");

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<AccountDto>>();
    }
}

