using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class NotificacaoCreateMapper : Mapper<NotificacaoCreateRequestModel>, INotificacaoCreateMapper
    {
        private readonly Notificacao _notificacao;

        public NotificacaoCreateMapper()
        {
            _notificacao = new Notificacao();
        }

        public Notificacao BuscarNotificacao()
        {
            _notificacao.tipo = BaseMapping.tipo;
            _notificacao.titulo = BaseMapping.titulo;
            _notificacao.descricao = BaseMapping.descricao;
            _notificacao.horario = BaseMapping.horario;
            _notificacao.tratamento_id = BaseMapping.tratamento_id;
            _notificacao.remedio_id = BaseMapping.remedio_id;
            return _notificacao;
        }
    }
}
