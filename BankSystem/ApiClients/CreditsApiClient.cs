using BankSystem.ApiClients;
using DTO;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class CreditsApiClient : AbstractApiClient
{
    public CreditsApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/credits/add-credit
    /// </summary>
    public async Task<bool> AddCreditAsync(CreditDto creditDto)
    {
        var response = await client.PostAsJsonAsync("api/credits/add-credit", creditDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// PUT api/credits/update-credit
    /// </summary>
    public async Task<bool> UpdateCreditAsync(CreditDto creditDto)
    {
        var response = await client.PutAsJsonAsync("api/credits/update-credit", creditDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// GET api/credits/load-credits
    /// </summary>
    public async Task<List<CreditDto>?> LoadCreditsAsync()
    {
        var response = await client.GetAsync("api/credits/load-credits");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<CreditDto>>();
    }
    /// <summary>
    /// GET api/credits/load-creditStatuses
    /// </summary>
    public async Task<List<CreditStatusDto>?> LoadCreditStatusesAsync()
    {
        var response = await client.GetAsync("api/credits/load-creditStatuses");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<CreditStatusDto>>();
    }
    /// <summary>
    /// GET api/credits/filter-credit-by-status
    /// </summary>
    public async Task<List<CreditDto>?> FilterByStatusAsync(CreditStatusDto statusDto)
    {
        var url =
            $"api/credits/filter-credit-by-status?StatusName={statusDto.Name}";

        var response = await client.GetAsync(url);

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<CreditDto>>();
    }
}

