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
            _statusDispenser.Medication_Id = BaseMapping.medicamento_id;
            _statusDispenser.Treatment_Id = BaseMapping.Treatment_id;
            _statusDispenser.Total_Quantity_Box_Treatment = BaseMapping.quantidade_total_medication_caixa; 
            _statusDispenser.Total_Quantity_Medication_Box = BaseMapping.quantidade_total_caixa_Treatment; 
            _statusDispenser.Treatment_Interval_Hours = BaseMapping.intervalo_Treatment_horas; 
            _statusDispenser.Treatment_Interval_Days = BaseMapping.intervalo_Treatment_dias; 
            _statusDispenser.Quantity_Medicine_Per_Dosage = BaseMapping.quantidade_medication_por_dosagem; 
            _statusDispenser.Daily_Dosage_Frequency = BaseMapping.frenquecia_dosagem_diaria; 
            _statusDispenser.Total_Quantity_Medication_Dosage_Day = BaseMapping.quantidade_total_medication_dosagem_dia; 
            _statusDispenser.Total_Quantity_Medicamentos_Treatment = BaseMapping.quantidade_total_medications_Treatment; 
            _statusDispenser.Weekly_Medication_Quantity = BaseMapping.quantidade_medication_semanal; 
            _statusDispenser.Current_Quantity_Medication_Box_Treatment = BaseMapping.quantidade_atual_medication_caixa_Treatment; 
            _statusDispenser.Missing_Medicine_Quantity_To_End_Treatment = BaseMapping.quantidade_medication_faltante_para_fim_Treatment; 
            _statusDispenser.Quantity_Days_Missing_To_End_Treatment = BaseMapping.quantidade_dias_faltante_para_fim_Treatment; 
            _statusDispenser.Creation_Data = BaseMapping.data_criacao; 
            _statusDispenser.Weekly_Update_Date = BaseMapping.data_atualizacao_semanal; 
            _statusDispenser.Treatment_Start_Date = BaseMapping.data_inicio_Treatment; 
            _statusDispenser.Final_Date_Expected_Treatment = BaseMapping.data_final_previsto_Treatment; 
            _statusDispenser.Final_Date_Marked_Treatment = BaseMapping.data_final_marcado_Treatment; 
            _statusDispenser.Treatment_Status = BaseMapping.status_Treatment; 
            _statusDispenser.Treatment_User_Id = BaseMapping.user_id;
            return _statusDispenser;
        }
    }
}
