using System;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace LinkaPay.Application.Implementation
{
    public class FlutterwaveService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FlutterwaveService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<FlutterwavePaymentResponse> InitiatePayment(FlutterwavePaymentRequest request)
        {
            var url = $"{_configuration["AppSettings:FlutterwaveApiBaseUrl"]}/payments";
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["AppSettings:FlutterwaveSecretKey"]}");

            try
            {
                Console.WriteLine($"Initiating payment with Flutterwave: {System.Text.Json.JsonSerializer.Serialize(request)}");

                var response = await _httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Flutterwave API returned a bad request: {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<FlutterwavePaymentResponse>();
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Error initiating payment: {httpEx.Message}");
            }
        }

    }

}

