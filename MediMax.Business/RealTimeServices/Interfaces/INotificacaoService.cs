using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.RealTimeServices.Interfaces
{
    public interface INotificacaoService
    {
        //Task<int> SetToRead(int[] notificationIds);
        //Task<int> SetToReadAll(int ownerId);
        Task<int> RegistarNotificacao(NotificacaoCreateRequestModel request);
    }
}