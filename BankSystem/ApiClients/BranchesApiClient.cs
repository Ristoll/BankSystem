using BankSystem.ApiClients;
using DTO;
using Octokit;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class BranchesApiClient : AbstractApiClient
{
    public BranchesApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/branches/add-branch
    /// </summary>
    public async Task<bool> AddBranchAsync(BankBranchDto branchDto)
    {
        var response = await client.PostAsJsonAsync("api/branches/add-branch", branchDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// PUT api/branches/update-branch
    /// </summary>
    public async Task<bool> UpdateBranchAsync(BankBranchDto branchDto)
    {
        var response = await client.PutAsJsonAsync("api/branches/update-branch", branchDto);
        return await HandleErrorAsync(response);
    }
    /// <summary>
    /// GET api/accounts/load-branches
    /// </summary>
    public async Task<List<BankBranchDto>?> LoadBranchesAsync()
    {
        var response = await client.GetAsync("api/accounts/load-branches");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<BankBranchDto>>();
    }
    /// <summary>
    /// GET api/accounts/load-branchTypes
    /// </summary>
    public async Task<List<BranchTypeDto>?> LoadBranchTypesAsync()
    {
        var response = await client.GetAsync("api/accounts/load-branchTypes");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<BranchTypeDto>>();
    }
    /// <summary>
    /// DELETE api/branches/delete-branch/{branchId}
    /// </summary>
    public async Task<bool> DeleteBranchAsync(int branchId)
    {
        var response = await client.DeleteAsync($"api/branches/delete-branch/{branchId}");
        return await HandleErrorAsync(response);
    }
}
