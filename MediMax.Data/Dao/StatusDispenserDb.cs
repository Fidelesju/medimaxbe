using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class StatusDispenserDb : Db<StatusDispenserResponseModel>, IStatusDispenserDb
    {
        public StatusDispenserDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<StatusDispenserResponseModel> BuscandoSeExisteAbastacimentoCadastrado(int tratamentoId)
        {
            string sql;
            StatusDispenserResponseModel medicamentoLista;
            sql = $@"
               SELECT  
                    id AS Id,
                    tratamento_id AS TratamentoId,
                    medicamento_id AS MedicamentoId,
                    quantidade_total_medicamento_caixa AS QuantidadeTotalMedicamentoCaixa,
                    quantidade_total_caixa_tratamento AS QuantidadeTotalCaixaTratamento,
                    quantidade_total_medicamento_dosagem_dia AS QuantidadeTotalMedicamentoDosagemDia,
                    quantidade_total_medicamentos_tratamento AS QuantidadeTotalMedicamentosTratamento,
                    intervalo_tratamento_horas AS IntervaloTratamentoHoras,
                    intervalo_tratamento_dias AS IntervaloTratamentoDias,
                    quantidade_medicamento_por_dosagem AS QuantidadeMedicamentoPorDosagem,
                    frenquecia_dosagem_diaria AS FrenqueciaDosagemDiaria,
                    quantidade_medicamento_semanal AS QuantidadeMedicamentoSemanal,
                    quantidade_atual_medicamento_caixa_tratamento AS QuantidadeAtualMedicamentoCaixaTratamento,
                    quantidade_medicamento_faltante_para_fim_tratamento AS QuantidadeMedicamentoFaltanteParaFimTratamento,
                    quantidade_dias_faltante_para_fim_tratamento AS QuantidadeDiasFaltanteParaFimTratamento,
                    data_criacao AS DataCriacao,
                    data_atualizacao_semanal AS DataAtualizacaoSemanal,
                    data_inicio_tratamento AS DataInicioTratamento,
                    data_final_previsto_tratamento AS DataFinalPrevistoTratamento,
                    data_final_marcado_tratamento AS DataFinalMarcadoTratamento,
                    status_tratamento AS StatusTratamento
                FROM status_dispenser
               WHERE tratamento_id = {tratamentoId}
               ORDER BY id DESC
               LIMIT 1
                ";
            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultObject();
            await Disconnect();
            return medicamentoLista;
        }


        protected override StatusDispenserResponseModel Mapper(DbDataReader reader)
        {
            StatusDispenserResponseModel statusDispenser = new StatusDispenserResponseModel();

            // Convertendo Id para int
            if (int.TryParse(reader["Id"].ToString(), out int id))
            {
                statusDispenser.Id = id;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo TratamentoId para int
            if (int.TryParse(reader["TratamentoId"].ToString(), out int tratamentoId))
            {
                statusDispenser.TratamentoId = tratamentoId;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo MedicamentoId para int
            if (int.TryParse(reader["MedicamentoId"].ToString(), out int medicamentoId))
            {
                statusDispenser.MedicamentoId = medicamentoId;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo QuantidadeTotalMedicamentoCaixa para int
            if (int.TryParse(reader["QuantidadeTotalMedicamentoCaixa"].ToString(), out int quantidadeTotalMedicamentoCaixa))
            {
                statusDispenser.QuantidadeTotalMedicamentoCaixa = quantidadeTotalMedicamentoCaixa;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo QuantidadeTotalCaixaTratamento para int
            if (int.TryParse(reader["QuantidadeTotalCaixaTratamento"].ToString(), out int quantidadeTotalCaixaTratamento))
            {
                statusDispenser.QuantidadeTotalCaixaTratamento = quantidadeTotalCaixaTratamento;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo IntervaloTratamentoHoras para int
            if (int.TryParse(reader["IntervaloTratamentoHoras"].ToString(), out int intervaloTratamentoHoras))
            {
                statusDispenser.IntervaloTratamentoHoras = intervaloTratamentoHoras;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo IntervaloTratamentoDias para int
            if (int.TryParse(reader["IntervaloTratamentoDias"].ToString(), out int intervaloTratamentoDias))
            {
                statusDispenser.IntervaloTratamentoDias = intervaloTratamentoDias;
            }
            else
            {
                // Tratar erro de conversão
            }
            
            if (int.TryParse(reader["QuantidadeMedicamentoPorDosagem"].ToString(), out int quantidadeMedicamentoPorDosagem))
            {
                statusDispenser.QuantidadeMedicamentoPorDosagem = quantidadeMedicamentoPorDosagem;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["FrenqueciaDosagemDiaria"].ToString(), out int frenqueciaDosagemDiaria))
            {
                statusDispenser.FrenqueciaDosagemDiaria = frenqueciaDosagemDiaria;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["QuantidadeTotalMedicamentoDosagemDia"].ToString(), out int quantidadeTotalMedicamentoDosagemDia))
            {
                statusDispenser.QuantidadeTotalMedicamentoDosagemDia = quantidadeTotalMedicamentoDosagemDia;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["QuantidadeTotalMedicamentosTratamento"].ToString(), out int quantidadeTotalMedicamentosTratamento))
            {
                statusDispenser.QuantidadeTotalMedicamentosTratamento = quantidadeTotalMedicamentosTratamento;
            }
            else
            {
                // Tratar erro de conversão
            }

           
             if (int.TryParse(reader["QuantidadeMedicamentoSemanal"].ToString(), out int quantidadeMedicamentoSemanal))
            {
                statusDispenser.QuantidadeMedicamentoSemanal = quantidadeMedicamentoSemanal;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["QuantidadeAtualMedicamentoCaixaTratamento"].ToString(), out int quantidadeAtualMedicamentoCaixaTratamento))
            {
                statusDispenser.QuantidadeAtualMedicamentoCaixaTratamento = quantidadeAtualMedicamentoCaixaTratamento;
            }
            else
            {
                // Tratar erro de conversão
            }

           
            if (int.TryParse(reader["QuantidadeMedicamentoFaltanteParaFimTratamento"].ToString(), out int quantidadeMedicamentoFaltanteParaFimTratamento))
            {
                statusDispenser.QuantidadeMedicamentoFaltanteParaFimTratamento = quantidadeMedicamentoFaltanteParaFimTratamento;
            }
            else
            {
                // Tratar erro de conversão
            }
             
            if (int.TryParse(reader["QuantidadeDiasFaltanteParaFimTratamento"].ToString(), out int quantidadeDiasFaltanteParaFimTratamento))
            {
                statusDispenser.QuantidadeDiasFaltanteParaFimTratamento = quantidadeDiasFaltanteParaFimTratamento;
            }
            else
            {
                // Tratar erro de conversão
            }

            statusDispenser.DataCriacao = reader["DataCriacao"].ToString();
            statusDispenser.DataAtualizacaoSemanal = reader["DataAtualizacaoSemanal"].ToString();
            statusDispenser.DataInicioTratamento = reader["DataInicioTratamento"].ToString();
            statusDispenser.DataFinalPrevistoTratamento = reader["DataFinalPrevistoTratamento"].ToString();
            statusDispenser.DataFinalMarcadoTratamento = reader["DataFinalMarcadoTratamento"].ToString();

            if (int.TryParse(reader["StatusTratamento"].ToString(), out int statusTratamento))
            {
                statusDispenser.StatusTratamento = statusTratamento;
            }
            else
            {
                // Tratar erro de conversão
            }
            return statusDispenser;
        }
    }
}
