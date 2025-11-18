using BankSystem.ApiClients;
using DTO;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class EmployeesApiClient : AbstractApiClient
{
    public EmployeesApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/employees/add-employee
    /// </summary>
    public async Task<bool> AddEmployeeAsync(EmployeeDto employeeDto)
    {
        var response = await client.PostAsJsonAsync("api/employees/add-employee", employeeDto);
        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// PUT api/employees/update-employee
    /// </summary>
    public async Task<bool> UpdateEmployeeAsync(EmployeeDto employeeDto)
    {
        var response = await client.PutAsJsonAsync("api/employees/update-employee", employeeDto);
        return await HandleErrorAsync(response);
    }
    /// <summary>
    /// GET api/employees/load-employees
    /// </summary>
    public async Task<List<EmployeeDto>?> LoadEmployeesAsync()
    {
        var response = await client.GetAsync("api/employees/load-employees");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
    }
    /// <summary>
    /// GET api/employees/load-employeeRoles
    /// </summary>
    public async Task<List<EmployeeRoleDto>?> LoadEmployeeRolesAsync()
    {
        var response = await client.GetAsync("api/employees/load-employeeRoles");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<EmployeeRoleDto>>();
    }
    /// <summary>
    /// DELETE api/employees/delete-employee?employeeId=...
    /// </summary>
    public async Task<bool> DeleteEmployeeAsync(int employeeId)
    {
        var response = await client.DeleteAsync(
            $"api/employees/delete-employee?employeeId={employeeId}");

        return await HandleErrorAsync(response);
    }

    /// <summary>
    /// POST api/employees/login-empployee
    /// </summary>
    public async Task<bool> LoginEmployeeAsync(string phone, string password)
    {
        var url = $"api/employees/login-employee?phone={Uri.EscapeDataString(phone)}&password={Uri.EscapeDataString(password)}";

        var response = await client.PostAsync(url, null);

        if (!await HandleErrorAsync(response)) return false;

        return await response.Content.ReadFromJsonAsync<bool>();
    }
}
