using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class TreatmentCreateMapper : Mapper<MedicamentoETreatmentCreateRequestModel>, ITreatmentCreateMapper
    {
        private readonly Treatment? _Treatment;

        public TreatmentCreateMapper()
        {
            _Treatment = new Treatment();
        }

        public Treatment GetTratemento(MedicamentoETreatmentCreateRequestModel request)
        {
            _Treatment.quantidade_medications = request.quantidade_medication_por_dia;
            _Treatment.horario_inicio = request.horario_inicial_Treatment;
            _Treatment.intervalo_Treatment = request.intervalo_Treatment_horas;
            _Treatment.tempo_Treatment_dias = request.intervalo_Treatment_dias;
            _Treatment.recomendacoes_alimentacao = request.recomendacoes_alimentacao;
            _Treatment.observacao = request.observacao;
            _Treatment.esta_ativo = 1;
            _Treatment.nome_medication = request.nome;
            return _Treatment;
        }
    }
}
