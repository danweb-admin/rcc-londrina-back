using System;
using Microsoft.Extensions.Logging;
using RccManager.Domain.Interfaces.Services;
using System.Net.Http.Json;
using RccManager.Domain.Responses;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace RccManager.Service.Services
{
  public class AsaasClient : IAsaasClient
  {
      private readonly HttpClient _http;
      private readonly ILogger<AsaasClient> _log;
      private string token;
      private string urlBase = "https://sandbox.asaas.com/api/v3";

      public AsaasClient(ILogger<AsaasClient> log)
      {
           token = Environment.GetEnvironmentVariable("AccessToken");
          _log = log;
      }

      public async Task<AsaasPaymentCreatedResponse> CreatePaymentAsync(AsaasCreatePaymentRequest req)
      {
          var options = new JsonSerializerOptions
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          };

          var json = JsonSerializer.Serialize(req,options);

          var content = new StringContent(json, Encoding.UTF8, "application/json");

          using var http = new HttpClient();
          http.DefaultRequestHeaders.Add("access_token", token);
          http.DefaultRequestHeaders.Add("User-Agent", "EventosRCC/1.0" );

          var res = await http.PostAsync($"{urlBase}/payments", content);

          var responseContent = await res.Content.ReadAsStringAsync();

          if (!res.IsSuccessStatusCode)
              throw new Exception($"Erro ao criar CreatePaymentAsync no Asaas: {responseContent}");

          return await res.Content.ReadFromJsonAsync<AsaasPaymentCreatedResponse>();
      }

      public async Task<AsaasPixResponse> GetPixQrCodeAsync(string paymentId)
      {
          using var http = new HttpClient();
          http.DefaultRequestHeaders.Add("access_token", token);
          http.DefaultRequestHeaders.Add("User-Agent", "EventosRCC/1.0" );

          var res = await http.GetAsync($"{urlBase}/payments/{paymentId}/pixQrCode");

          var responseContent = await res.Content.ReadAsStringAsync();

          if (!res.IsSuccessStatusCode)
              throw new Exception($"Erro ao criar GetPixQrCodeAsync no Asaas: {responseContent}");

          return await res.Content.ReadFromJsonAsync<AsaasPixResponse>();
      }

      public async Task SimulatePaymentAsync(string paymentId)
      {
          // sandbox only (no body)
          var res = await _http.PostAsync($"/payments/{paymentId}/simulatePayment", null);
          res.EnsureSuccessStatusCode();
      }

      public async Task<AsaasPaymentGetResponse> GetPaymentAsync(string paymentId)
      {
          var res = await _http.GetAsync($"/payments/{paymentId}");
          res.EnsureSuccessStatusCode();
          return await res.Content.ReadFromJsonAsync<AsaasPaymentGetResponse>();
      }

      public async Task<AsaasCustomerResponse> CriarClienteAsync(AsaasCustomerRequest request)
      {
          var options = new JsonSerializerOptions
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          };

          var result = await BuscarClientePorCpfAsync(request.CpfCnpj);

          if (result != null)
            return result;

          var json = JsonSerializer.Serialize(request,options);

          var content = new StringContent(json, Encoding.UTF8, "application/json");

          using var http = new HttpClient();
          http.DefaultRequestHeaders.Add("access_token", token);
          http.DefaultRequestHeaders.Add("User-Agent", "EventosRCC/1.0" );

          var response = await http.PostAsync($"{urlBase}/customers", content);

          var responseContent = await response.Content.ReadAsStringAsync();

          if (!response.IsSuccessStatusCode)
              throw new Exception($"Erro ao criar cliente no Asaas: {responseContent}");

          return JsonSerializer.Deserialize<AsaasCustomerResponse>(responseContent,options);
      }

      public async Task<AsaasCustomerResponse> BuscarClientePorCpfAsync(string cpfCnpj)
      {
          using var http = new HttpClient();
          http.DefaultRequestHeaders.Add("access_token", token);
          http.DefaultRequestHeaders.Add("User-Agent", "EventosRCC/1.0" );

          var response = await http.GetAsync($"{urlBase}/customers?cpfCnpj={cpfCnpj}");

          var content = await response.Content.ReadAsStringAsync();

          if (!response.IsSuccessStatusCode)
              throw new Exception($"Erro ao buscar cliente: {content}");

          var result = JsonSerializer.Deserialize<AsaasCustomerListResponse>(content);

          return result?.Data?.FirstOrDefault();
      }

      public async Task<AsaasPaymentResponse> CriarCobrancaCartaoAsync(AsaasCreatePaymentRequest request)
      {
          var options = new JsonSerializerOptions
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          };

          var body = new
          {
              customer = request.customer,
              billingType = "CREDIT_CARD",
              value = request.value,
              dueDate = request.dueDate.ToString("yyyy-MM-dd"),
              description = request.description
          };

          var json = JsonSerializer.Serialize(body);
          var content = new StringContent(json, Encoding.UTF8, "application/json");

          using var http = new HttpClient();
          http.DefaultRequestHeaders.Add("access_token", token);
          http.DefaultRequestHeaders.Add("User-Agent", "EventosRCC/1.0" );

          var response = await http.PostAsync($"{urlBase}/payments", content);
          var result = await response.Content.ReadAsStringAsync();

          if (!response.IsSuccessStatusCode)
              throw new Exception($"Erro ao criar CriarCobrancaCartaoAsync no Asaas: {result}");

          return JsonSerializer.Deserialize<AsaasPaymentResponse>(result,options);
      }

      public async Task<AsaasCardPaymentResult> PagarComCartaoAsync(string paymentId, CartaoDto cartao)
      {
          var options = new JsonSerializerOptions
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          };
          
          var body = new
          {
              creditCard = new
              {
                  holderName = cartao.Nome,
                  number = cartao.Numero,
                  expiryMonth = cartao.ValidadeMes,
                  expiryYear = cartao.ValidadeAno,
                  ccv = cartao.Cvv
              },
              creditCardHolderInfo = new
              {
                  name = cartao.CreditCardHolderInfo.Name,
                  email = cartao.CreditCardHolderInfo.Email,
                  cpfCnpj = cartao.CreditCardHolderInfo.CpfCnpj,
                  postalCode = cartao.CreditCardHolderInfo.PostalCode,
                  addressNumber = cartao.CreditCardHolderInfo.AddressNumber,
                  phone = cartao.CreditCardHolderInfo.Phone
              }
          };

          var json = JsonSerializer.Serialize(body);
          var content = new StringContent(json, Encoding.UTF8, "application/json");

          using var http = new HttpClient();
          http.DefaultRequestHeaders.Add("access_token", token);
          http.DefaultRequestHeaders.Add("User-Agent", "EventosRCC/1.0" );

          var response = await http.PostAsync($"{urlBase}/payments/{paymentId}/payWithCreditCard", content);

          var result = await response.Content.ReadAsStringAsync();

          if (!response.IsSuccessStatusCode)
              throw new Exception($"Erro ao criar PagarComCartaoAsync no Asaas: {result}");

          return JsonSerializer.Deserialize<AsaasCardPaymentResult>(result,options);
      }
  }
}

