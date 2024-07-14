using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class NotificationCreateMapper : Mapper<NotificationCreateRequestModel>, INotificationCreateMapper
    {
        private readonly Notification _Notification;

        public NotificationCreateMapper()
        {
            _Notification = new Notification();
        }

        public Notification BuscarNotificacao ( )
        {
            _Notification.tipo = BaseMapping.tipo;
            _Notification.titulo = BaseMapping.titulo;
            _Notification.descricao = BaseMapping.descricao;
            _Notification.horario = BaseMapping.horario;
            _Notification.Treatment_id = BaseMapping.Treatment_id;
            _Notification.remedio_id = BaseMapping.remedio_id;
            return _Notification;
        }
    }
}
