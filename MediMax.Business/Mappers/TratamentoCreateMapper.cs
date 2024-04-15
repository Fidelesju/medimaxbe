using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class TratamentoCreateMapper : Mapper<MedicamentoETratamentoCreateRequestModel>, ITratamentoCreateMapper
    {
        private readonly Tratamento? _tratamento;

        public TratamentoCreateMapper()
        {
            _tratamento = new Tratamento();
        }

        public Tratamento BuscarTratemento(MedicamentoETratamentoCreateRequestModel request, int remedio_id)
        {
            _tratamento.quantidade_medicamentos = request.quantidade_medicamento_dosagem;
            _tratamento.horario_inicio = request.horario_inicial_tratamento;
            _tratamento.intervalo_tratamento = request.intervalo_tratamento_horas;
            _tratamento.tempo_tratamento_dias = request.intervalo_tratamento_dias;
            _tratamento.recomendacoes_alimentacao = request.recomendacoes_alimentacao;
            _tratamento.observacao = request.observacao;
            _tratamento.esta_ativo = 1;
            _tratamento.remedio_id = remedio_id;
            _tratamento.nome_medicamento = request.nome;
            return _tratamento;
        }
    }
}
