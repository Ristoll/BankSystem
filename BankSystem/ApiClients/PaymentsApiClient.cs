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
    /// <summary>
    /// GET api/payments/load-payments
    /// </summary>
    public async Task<List<PaymentDto>?> LoadPaymentsAsync()
    {
        var response = await client.GetAsync("api/payments/load-payments");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<PaymentDto>>();
    }
    /// <summary>
    /// GET api/payments/load-paymentTypes
    /// </summary>
    public async Task<List<PaymentTypeDto>?> LoadPaymentTypesAsync()
    {
        var response = await client.GetAsync("api/payments/load-paymentTypes");

        if (!await HandleErrorAsync(response))
            return null;

        return await response.Content.ReadFromJsonAsync<List<PaymentTypeDto>>();
    }
}
