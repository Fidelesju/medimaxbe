using Microsoft.AspNetCore.SignalR;

namespace MediMax.Business.RealTimeServices
{
    public class NotificacaoHubService : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}