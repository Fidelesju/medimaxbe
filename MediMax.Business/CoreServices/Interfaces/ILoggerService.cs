namespace MediMax.Business.CoreServices.Interfaces
{
    public interface ILoggerService
    {
        Task LogError(Exception exception, HttpContext context);
        Task LogErrorServicesBackground(Exception exception);
        Task LogInfo(string info);
    }
}
