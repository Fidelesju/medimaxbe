using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class TreatmentUpdateMapper : Mapper<TreatmentUpdateRequestModel>, ITreatmentUpdateMapper
    {
        private readonly Treatment? _treatment;

        public TreatmentUpdateMapper ( )
        {
            _treatment = new Treatment();
        }

        public Treatment GetTreatment()
        {
            _treatment.id = BaseMapping.treatment_id;
            _treatment.quantidade_medicamentos = BaseMapping.medication_quantity;
            _treatment.horario_inicio = BaseMapping.treatment_start_time;
            _treatment.intervalo_tratamento = BaseMapping.treatment_interval_hours;
            _treatment.tempo_tratamento_dias= BaseMapping.treatment_interval_days;
            _treatment.recomendacoes_alimentacao = BaseMapping.dietary_recommendations;
            _treatment.observacao = BaseMapping.observation;
            _treatment.esta_ativo = 1;
            _treatment.remedio_id = BaseMapping.medication_id;
            _treatment.nome_medicamento = BaseMapping.medicine_name;
            return _treatment;
        }
    }
}
