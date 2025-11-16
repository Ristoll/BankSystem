using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace BankSystem.ApiClients;

public class AbstractApiClient
{
    protected readonly HttpClient client;

    protected AbstractApiClient(HttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        this.client = client;
    }

    protected async Task<bool> HandleErrorAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            MessageBox.Show(error);
        }

        return response.IsSuccessStatusCode;
    }
}
