namespace MediMax.Business.RealTimeServices.Interfaces
{
    public interface INotificationService
    {
        Task<int> NotifyUserAsync ( int userId, string message );
        Task<bool> SendNotificationToEmail ( string email, string title, string subjectEmail, string bodyEmail );
    }
}
