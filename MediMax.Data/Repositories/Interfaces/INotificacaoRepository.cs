using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface INotificacaoRepository
    {
        int Create(Notificacao notificacao);
        void Update(Notificacao notificacao);
        //Task<int> SetToRead(int notificationId);
        //Task<int> SetToReadAll(int ownerId);
    }
}
