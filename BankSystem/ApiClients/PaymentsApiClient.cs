using BankSystem.ApiClients;
using DTO;
using System.Net.Http.Json;

namespace BankSystem.ApiClients;

public class PaymentsApiClient : AbstractApiClient
{
    public PaymentsApiClient(HttpClient client) : base(client)
    {
    }

    /// <summary>
    /// POST api/payments/add-payment
    /// </summary>
    public async Task<bool> AddPaymentAsync(PaymentDto paymentDto)
    {
        var response = await client.PostAsJsonAsync("api/payments/add-payment", paymentDto);
        return await HandleErrorAsync(response);
    }
}
