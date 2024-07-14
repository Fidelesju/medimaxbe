using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class DispenserStatusCreateMapper : Mapper<DispenserStatusCreateRequestModel>, IDispenserStatusCreateMapper
    {
        private readonly StatusDispenser _statusDispenser;

        public DispenserStatusCreateMapper()
        {
            _statusDispenser = new StatusDispenser();
        }

        public StatusDispenser BuscarDispenserStatus ()
        {
            _statusDispenser.MedicationId = BaseMapping.medicamento_id;
            _statusDispenser.TreatmentId = BaseMapping.Treatment_id;
            _statusDispenser.TotalQuantityBoxTreatment = BaseMapping.quantidade_total_medication_caixa; 
            _statusDispenser.TotalQuantityMedicationBox = BaseMapping.quantidade_total_caixa_Treatment; 
            _statusDispenser.TreatmentIntervalHours = BaseMapping.intervalo_Treatment_horas; 
            _statusDispenser.TreatmentIntervalDays = BaseMapping.intervalo_Treatment_dias; 
            _statusDispenser.QuantityMedicinePerDosage = BaseMapping.quantidade_medication_por_dosagem; 
            _statusDispenser.DailyDosageFrequency = BaseMapping.frenquecia_dosagem_diaria; 
            _statusDispenser.TotalQuantityMedicationDosageDay = BaseMapping.quantidade_total_medication_dosagem_dia; 
            _statusDispenser.TotalQuantityMedicamentosTreatment = BaseMapping.quantidade_total_medications_Treatment; 
            _statusDispenser.WeeklyMedicationQuantity = BaseMapping.quantidade_medication_semanal; 
            _statusDispenser.CurrentQuantityMedicationBoxTreatment = BaseMapping.quantidade_atual_medication_caixa_Treatment; 
            _statusDispenser.MissingMedicineQuantityToEndTreatment = BaseMapping.quantidade_medication_faltante_para_fim_Treatment; 
            _statusDispenser.QuantityDaysMissingToEndTreatment = BaseMapping.quantidade_dias_faltante_para_fim_Treatment; 
            _statusDispenser.CreationData = BaseMapping.data_criacao; 
            _statusDispenser.WeeklyUpdateDate = BaseMapping.data_atualizacao_semanal; 
            _statusDispenser.TreatmentStartDate = BaseMapping.data_inicio_Treatment; 
            _statusDispenser.FinalDateExpectedTreatment = BaseMapping.data_final_previsto_Treatment; 
            _statusDispenser.FinalDateMarkedTreatment = BaseMapping.data_final_marcado_Treatment; 
            _statusDispenser.TreatmentStatus = BaseMapping.status_Treatment; 
            return _statusDispenser;
        }
    }
}
