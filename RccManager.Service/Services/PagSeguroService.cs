using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.Service.Services
{
	public class PagSeguroService : IPagSeguroService
	{
        private readonly IEventoRepository _eventoRepository;

        public PagSeguroService(IEventoRepository eventoRepository)
		{
            _eventoRepository = eventoRepository;

        }

        public async Task<PagSeguroResponse> GerarLinkPagamentoAsync(InscricaoDto inscricao)
        {
            string token = Environment.GetEnvironmentVariable("Token");
            string url = Environment.GetEnvironmentVariable("UrlPagSeguro");

            Evento evento = await _eventoRepository.GetById(inscricao.EventoId);

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var body = new
            {
                reference_id = inscricao.CodigoInscricao,

                customer = new
                {
                    name = inscricao.Nome,
                    email = inscricao.Email,
                    tax_id = inscricao.Cpf.Replace(".","").Replace("-",""), 
                    phones = new[]
                        {
                            new {
                                country = "55",
                                area = inscricao.Telefone.Replace("(","").Replace(")","").Replace("-","").Replace(" ","").Substring(0, 2),
                                number = inscricao.Telefone.Replace("(","").Replace(")","").Replace("-","").Replace(" ","").Substring(2),
                                type = "MOBILE"
                            }
                        }       
                },
                items = new[]
                    {
                        new {
                            name = evento.Nome,
                            quantity = 1,
                            unit_amount = (int)(inscricao.ValorInscricao * 100) // em centavos
                        }
                    },

                qr_codes = new[]
                {
                    new {
                        amount = new { value = (int)(inscricao.ValorInscricao * 100) },
                        expiration_date = DateTime.Now.AddDays(1)
                            .ToString("yyyy-MM-ddTHH:mm:sszzz") // formato ISO
                    }
                },
                notification_urls = new[]
                {
                    "https://backend.rcc-londrina.online/api/webhook"
                }
            };

            Console.WriteLine("request: " + body.ToString());

            var response = await http.PostAsJsonAsync($"{url}/orders", body);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine("response: " + await response.Content.ReadAsStringAsync());

            Console.WriteLine("response: " + await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadFromJsonAsync<PagSeguroResponse>();

           
            return result;
        }

        public async Task<PagSeguroResponse> GerarPagamentoCartaoAsync(InscricaoDto inscricao)
        {
            string token = Environment.GetEnvironmentVariable("Token");
            string url = Environment.GetEnvironmentVariable("UrlPagSeguro");

            Evento evento = await _eventoRepository.GetById(inscricao.EventoId);

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Normalizando CPF
            string cpfSomenteNumeros = inscricao.Cpf
                .Replace(".", "")
                .Replace("-", "");

            // Normalizando telefone
            string telefoneSomenteNumeros = inscricao.Telefone
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace(" ", "");

            string ddd = telefoneSomenteNumeros.Substring(0, 2);
            string numeroTelefone = telefoneSomenteNumeros.Substring(2);

            var body = new
            {
                reference_id = inscricao.CodigoInscricao,

                customer = new
                {
                    name = inscricao.Nome,
                    email = inscricao.Email,
                    tax_id = cpfSomenteNumeros,
                    phones = new[]
                        {
                        new {
                            country = "55",
                            area = ddd,
                            number = numeroTelefone,
                            type = "MOBILE"
                        }
                    }
                },

                items = new[]
                    {
                        new {
                            reference_id = "inscricao-" + inscricao.CodigoInscricao,
                            name = evento.Nome,
                            quantity = 1,
                            unit_amount = (int)(inscricao.ValorInscricao * 100)
                        }
                },

                notification_urls = new[]
                {
                    "https://backend.rcc-londrina.online/api/webhook"
                },

                charges = new[]
                    {
                        new {
                            reference_id = "cobranca-" + inscricao.CodigoInscricao,
                            description = $"Pagamento inscrição {evento.Nome}",
                            amount = new {
                                value = (int)(inscricao.ValorInscricao * 100),
                                currency = "BRL"
                            },
                            payment_method = new {
                                type = "CREDIT_CARD",
                                //installments = inscricao.QuantidadeParcelas == 0 ? 1 : inscricao.QuantidadeParcelas,
                                capture = true,
                                //card = new {
                                //    number = inscricao.NumeroCartao,
                                //    exp_month = inscricao.Validade.Substring(0, 2),
                                //    exp_year = "20" + inscricao.Validade.Substring(2),
                                //    security_code = inscricao.Cvv,
                                //    store = false
                                //},
                                holder = new {
                                    name = inscricao.Nome,
                                    tax_id = cpfSomenteNumeros
                                }
                            }
                    }
                }
            };

            Console.WriteLine("request: " + body.ToString());


            var response = await http.PostAsJsonAsync($"{url}/orders", body);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("response: " + await response.Content.ReadAsStringAsync());
                var error = await response.Content.ReadFromJsonAsync<PagSeguroErrorResponse>();
                throw new WebException("Houve um problema para efetuar a transação, verifique os dados do cartão.");
            }

            Console.WriteLine("response: " + await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadFromJsonAsync<PagSeguroResponse>();

            return result!;
        }

    }
}



