using MediMax.Data.RequestModels;

namespace MediMax.Business.RealTimeServices.Interfaces
{
    public interface IFirebaseEnvioNotificacaoService
    {
        public Task<bool> SendPushNotificationToUser<T>(EnvioNotificacaoFirebaseRequestModel<T> notification) where T : class;
    }
}
