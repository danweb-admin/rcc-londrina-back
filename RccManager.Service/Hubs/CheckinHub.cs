using System;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace RccManager.Service.Hubs
{
    public class CheckinHub : Hub
{
    // usuário entrou na conexão
    public override async Task OnConnectedAsync()
    {
        var email = Context.User?.FindFirst(ClaimTypes.Email)?.Value;

        Console.WriteLine($"Usuário conectado: {email}");

        await base.OnConnectedAsync();
    }

    // entrar no grupo do evento
    public async Task EntrarNoEvento(string eventoId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"evento-{eventoId}");
    }

    // sair do evento
    public async Task SairDoEvento(string eventoId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"evento-{eventoId}");
    }
}
}

