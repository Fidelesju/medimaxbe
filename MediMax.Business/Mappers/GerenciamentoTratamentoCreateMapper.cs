using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class GerenciamentoTratamentoCreateMapper : Mapper<GerencimentoTratamentoCreateRequestModel>, IGerenciamentoTratamentoCreateMapper
    {
        private readonly GerenciamentoTratamento? _gerenciamentoTratamento;

        public GerenciamentoTratamentoCreateMapper()
        {
            _gerenciamentoTratamento = new GerenciamentoTratamento();
        }

        public GerenciamentoTratamento GetGerenciamentoTratamento()
        {
            _gerenciamentoTratamento.tratamento_id = BaseMapping.tratamento_id;
            _gerenciamentoTratamento.horario_correto_tratamento = BaseMapping.horario_correto_tratamento;
            _gerenciamentoTratamento.horario_ingestao_medicamento = BaseMapping.horario_ingestao_medicamento;
            _gerenciamentoTratamento.data_ingestao_medicamento = BaseMapping.data_ingestao_medicamento;
            _gerenciamentoTratamento.foi_tomado = BaseMapping.foi_tomado;
            return _gerenciamentoTratamento;
        }
    }
}
