using BankSystem.ApiClients;
using DTO;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class ClientsApiClient : AbstractApiClient
{
    public ClientsApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/clients/add-client
    /// </summary>
    public async Task<bool> AddClientAsync(ClientDto clientDto)
    {
        var response = await client.PostAsJsonAsync("api/clients/add-client", clientDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// PUT api/clients/update-client
    /// </summary>
    public async Task<bool> UpdateClientAsync(ClientDto clientDto)
    {
        var response = await client.PutAsJsonAsync("api/clients/update-client", clientDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// GET api/clients/search-client-by-fullname?clientName=...
    /// </summary>
    public async Task<List<ClientDto>?> SearchByFullNameAsync(string clientName)
    {
        var response = await client.GetAsync(
            $"api/clients/search-client-by-fullname?clientName={clientName}");

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<ClientDto>>();
    }

    /// <summary>
    /// GET api/clients/search-client-by-phonenumber?phoneNumber=...
    /// </summary>
    public async Task<List<ClientDto>?> SearchByPhoneNumberAsync(string phoneNumber)
    {
        var response = await client.GetAsync(
            $"api/clients/search-client-by-phonenumber?phoneNumber={phoneNumber}");

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<ClientDto>>();
    }

    /// <summary>
    /// GET api/clients/filter-client-by-accounttype
    /// </summary>
    public async Task<List<ClientDto>?> FilterByAccountTypeAsync(AccountTypeDto accountTypeDto)
    {
        // Розбираємо у query параметри accountTypeDto
        var url =
            $"api/clients/filter-client-by-accounttype?TypeName={accountTypeDto.Name}";

        var response = await client.GetAsync(url);

        if (!await HandleErrorAsync(response)) return null;

        return await response.Content.ReadFromJsonAsync<List<ClientDto>>();
    }
}

