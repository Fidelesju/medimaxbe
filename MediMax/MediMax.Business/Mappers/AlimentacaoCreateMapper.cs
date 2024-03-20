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

            _alimentacao.tipo_refeicao = BaseMapping.tipo_refeicao;
            _alimentacao.horario = BaseMapping.horario;
            _alimentacao.alimento = BaseMapping.alimento;
            _alimentacao.quantidade = BaseMapping.quantidade;
            _alimentacao.unidade_medida = BaseMapping.unidade_medida;
            return _alimentacao;
        }
    }
}
