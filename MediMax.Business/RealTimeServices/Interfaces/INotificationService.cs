namespace MediMax.Business.RealTimeServices.Interfaces
{
    public interface INotificationService
    {
        Task<int> NotifyUserAsync ( int userId, string message );
    }
}
