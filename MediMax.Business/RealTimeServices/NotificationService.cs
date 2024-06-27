using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Data.ApplicationModels;
using Microsoft.AspNetCore.SignalR;

namespace MediMax.Business.RealTimeServices
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService ( IHubContext<NotificationHub> hubContext )
        {
            _hubContext = hubContext;
        }

        public async Task<int> NotifyUserAsync ( int userId, string message )
        {
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message);
            return userId;
        }
    }
}