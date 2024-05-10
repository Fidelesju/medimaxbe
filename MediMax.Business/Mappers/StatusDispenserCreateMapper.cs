using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class StatusDispenserCreateMapper : Mapper<StatusDispenserCreateRequestModel>, IStatusDispenserCreateMapper
    {
        private readonly StatusDispenser _statusDispenser;

        public StatusDispenserCreateMapper()
        {
            _statusDispenser = new StatusDispenser();
        }

        public StatusDispenser BuscarStatusDispenser()
        {
            _statusDispenser.medicamento_id = BaseMapping.medicamento_id;
            _statusDispenser.tratamento_id = BaseMapping.tratamento_id;
            _statusDispenser.quantidade_total_medicamento_caixa = BaseMapping.quantidade_total_medicamento_caixa; 
            _statusDispenser.quantidade_total_caixa_tratamento = BaseMapping.quantidade_total_caixa_tratamento; 
            _statusDispenser.intervalo_tratamento_horas = BaseMapping.intervalo_tratamento_horas; 
            _statusDispenser.intervalo_tratamento_dias = BaseMapping.intervalo_tratamento_dias; 
            _statusDispenser.quantidade_medicamento_por_dosagem = BaseMapping.quantidade_medicamento_por_dosagem; 
            _statusDispenser.frenquecia_dosagem_diaria = BaseMapping.frenquecia_dosagem_diaria; 
            _statusDispenser.quantidade_total_medicamento_dosagem_dia = BaseMapping.quantidade_total_medicamento_dosagem_dia; 
            _statusDispenser.quantidade_total_medicamentos_tratamento = BaseMapping.quantidade_total_medicamentos_tratamento; 
            _statusDispenser.quantidade_medicamento_semanal = BaseMapping.quantidade_medicamento_semanal; 
            _statusDispenser.quantidade_atual_medicamento_caixa_tratamento = BaseMapping.quantidade_atual_medicamento_caixa_tratamento; 
            _statusDispenser.quantidade_medicamento_faltante_para_fim_tratamento = BaseMapping.quantidade_medicamento_faltante_para_fim_tratamento; 
            _statusDispenser.quantidade_dias_faltante_para_fim_tratamento = BaseMapping.quantidade_dias_faltante_para_fim_tratamento; 
            _statusDispenser.data_criacao = BaseMapping.data_criacao; 
            _statusDispenser.data_atualizacao_semanal = BaseMapping.data_atualizacao_semanal; 
            _statusDispenser.data_inicio_tratamento = BaseMapping.data_inicio_tratamento; 
            _statusDispenser.data_final_previsto_tratamento = BaseMapping.data_final_previsto_tratamento; 
            _statusDispenser.data_final_marcado_tratamento = BaseMapping.data_final_marcado_tratamento; 
            _statusDispenser.status_tratamento = BaseMapping.status_tratamento; 
            return _statusDispenser;
        }
    }
}
