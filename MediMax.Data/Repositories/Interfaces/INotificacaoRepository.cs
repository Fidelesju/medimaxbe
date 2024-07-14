using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        int Create(Notification Notification);
        void Update(Notification Notification);
        //Task<int> SetToRead(int notificationId);
        //Task<int> SetToReadAll(int ownerId);
    }
}
