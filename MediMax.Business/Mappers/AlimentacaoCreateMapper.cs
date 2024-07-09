using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class AlimentacaoCreateMapper : Mapper<AlimentacaoCreateRequestModel>, IAlimentacaoCreateMapper
    {
        private readonly Alimentacao? _alimentacao;

        public AlimentacaoCreateMapper()
        {
            _alimentacao = new Alimentacao();
        }

        public Alimentacao GetFood()
        {
            _alimentacao.usuarioId = BaseMapping.usuarioId;
            _alimentacao.tipo_refeicao = BaseMapping.tipo_refeicao;
            _alimentacao.horario = BaseMapping.horario;
            _alimentacao.detalhe_alimentacao_id = BaseMapping.detalhe_alimentacao_id;
            return _alimentacao;
        }
    }
}
