using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class DispenserStatusCreateMapper : Mapper<DispenserStatusCreateRequestModel>, IDispenserStatusCreateMapper
    {
        private readonly DispenserStatus _DispenserStatus;

        public DispenserStatusCreateMapper()
        {
            _DispenserStatus = new DispenserStatus();
        }

        public DispenserStatus BuscarDispenserStatus()
        {
            _DispenserStatus.medicamento_id = BaseMapping.medicamento_id;
            _DispenserStatus.Treatment_id = BaseMapping.Treatment_id;
            _DispenserStatus.quantidade_total_medication_caixa = BaseMapping.quantidade_total_medication_caixa; 
            _DispenserStatus.quantidade_total_caixa_Treatment = BaseMapping.quantidade_total_caixa_Treatment; 
            _DispenserStatus.intervalo_Treatment_horas = BaseMapping.intervalo_Treatment_horas; 
            _DispenserStatus.intervalo_Treatment_dias = BaseMapping.intervalo_Treatment_dias; 
            _DispenserStatus.quantidade_medication_por_dosagem = BaseMapping.quantidade_medication_por_dosagem; 
            _DispenserStatus.frenquecia_dosagem_diaria = BaseMapping.frenquecia_dosagem_diaria; 
            _DispenserStatus.quantidade_total_medication_dosagem_dia = BaseMapping.quantidade_total_medication_dosagem_dia; 
            _DispenserStatus.quantidade_total_medications_Treatment = BaseMapping.quantidade_total_medications_Treatment; 
            _DispenserStatus.quantidade_medication_semanal = BaseMapping.quantidade_medication_semanal; 
            _DispenserStatus.quantidade_atual_medication_caixa_Treatment = BaseMapping.quantidade_atual_medication_caixa_Treatment; 
            _DispenserStatus.quantidade_medication_faltante_para_fim_Treatment = BaseMapping.quantidade_medication_faltante_para_fim_Treatment; 
            _DispenserStatus.quantidade_dias_faltante_para_fim_Treatment = BaseMapping.quantidade_dias_faltante_para_fim_Treatment; 
            _DispenserStatus.data_criacao = BaseMapping.data_criacao; 
            _DispenserStatus.data_atualizacao_semanal = BaseMapping.data_atualizacao_semanal; 
            _DispenserStatus.data_inicio_Treatment = BaseMapping.data_inicio_Treatment; 
            _DispenserStatus.data_final_previsto_Treatment = BaseMapping.data_final_previsto_Treatment; 
            _DispenserStatus.data_final_marcado_Treatment = BaseMapping.data_final_marcado_Treatment; 
            _DispenserStatus.status_Treatment = BaseMapping.status_Treatment; 
            return _DispenserStatus;
        }
    }
}
