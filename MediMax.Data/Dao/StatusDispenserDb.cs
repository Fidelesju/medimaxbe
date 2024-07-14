using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class DispenserStatusDb : Db<DispenserStatusResponseModel>, IDispenserStatusDb
    {
        public DispenserStatusDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<DispenserStatusResponseModel> BuscandoSeExisteAbastacimentoCadastrado(int TreatmentId)
        {
            string sql;
            DispenserStatusResponseModel medicamentoLista;
            sql = $@"
               SELECT  
                    id AS Id,
                    Treatment_id AS TreatmentId,
                    medicamento_id AS MedicamentoId,
                    quantidade_total_medication_caixa AS QuantidadeTotalMedicamentoCaixa,
                    quantidade_total_caixa_Treatment AS QuantidadeTotalCaixaTreatment,
                    quantidade_total_medication_dosagem_dia AS QuantidadeTotalMedicamentoDosagemDia,
                    quantidade_total_medications_Treatment AS QuantidadeTotalMedicamentosTreatment,
                    intervalo_Treatment_horas AS IntervaloTreatmentHoras,
                    intervalo_Treatment_dias AS IntervaloTreatmentDias,
                    quantidade_medication_por_dosagem AS QuantidadeMedicamentoPorDosagem,
                    frenquecia_dosagem_diaria AS FrenqueciaDosagemDiaria,
                    quantidade_medication_semanal AS QuantidadeMedicamentoSemanal,
                    quantidade_atual_medication_caixa_Treatment AS QuantidadeAtualMedicamentoCaixaTreatment,
                    quantidade_medication_faltante_para_fim_Treatment AS QuantidadeMedicamentoFaltanteParaFimTreatment,
                    quantidade_dias_faltante_para_fim_Treatment AS QuantidadeDiasFaltanteParaFimTreatment,
                    data_criacao AS DataCriacao,
                    data_atualizacao_semanal AS DataAtualizacaoSemanal,
                    data_inicio_Treatment AS DataInicioTreatment,
                    data_final_previsto_Treatment AS DataFinalPrevistoTreatment,
                    data_final_marcado_Treatment AS DataFinalMarcadoTreatment,
                    status_Treatment AS StatusTreatment
                FROM status_dispenser
               WHERE Treatment_id = {TreatmentId}
               ORDER BY id DESC
               LIMIT 1
                ";
            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultObject();
            await Disconnect();
            return medicamentoLista;
        }


        protected override DispenserStatusResponseModel Mapper(DbDataReader reader)
        {
            DispenserStatusResponseModel DispenserStatus = new DispenserStatusResponseModel();

            // Convertendo Id para int
            if (int.TryParse(reader["Id"].ToString(), out int id))
            {
                DispenserStatus.Id = id;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo TreatmentId para int
            if (int.TryParse(reader["TreatmentId"].ToString(), out int TreatmentId))
            {
                DispenserStatus.TreatmentId = TreatmentId;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo MedicamentoId para int
            if (int.TryParse(reader["MedicamentoId"].ToString(), out int medicamentoId))
            {
                DispenserStatus.MedicamentoId = medicamentoId;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo QuantidadeTotalMedicamentoCaixa para int
            if (int.TryParse(reader["QuantidadeTotalMedicamentoCaixa"].ToString(), out int quantidadeTotalMedicamentoCaixa))
            {
                DispenserStatus.QuantidadeTotalMedicamentoCaixa = quantidadeTotalMedicamentoCaixa;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo QuantidadeTotalCaixaTreatment para int
            if (int.TryParse(reader["QuantidadeTotalCaixaTreatment"].ToString(), out int quantidadeTotalCaixaTreatment))
            {
                DispenserStatus.QuantidadeTotalCaixaTreatment = quantidadeTotalCaixaTreatment;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo IntervaloTreatmentHoras para int
            if (int.TryParse(reader["IntervaloTreatmentHoras"].ToString(), out int intervaloTreatmentHoras))
            {
                DispenserStatus.IntervaloTreatmentHoras = intervaloTreatmentHoras;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo IntervaloTreatmentDias para int
            if (int.TryParse(reader["IntervaloTreatmentDias"].ToString(), out int intervaloTreatmentDias))
            {
                DispenserStatus.IntervaloTreatmentDias = intervaloTreatmentDias;
            }
            else
            {
                // Tratar erro de conversão
            }
            
            if (int.TryParse(reader["QuantidadeMedicamentoPorDosagem"].ToString(), out int quantidadeMedicamentoPorDosagem))
            {
                DispenserStatus.QuantidadeMedicamentoPorDosagem = quantidadeMedicamentoPorDosagem;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["FrenqueciaDosagemDiaria"].ToString(), out int frenqueciaDosagemDiaria))
            {
                DispenserStatus.FrenqueciaDosagemDiaria = frenqueciaDosagemDiaria;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["QuantidadeTotalMedicamentoDosagemDia"].ToString(), out int quantidadeTotalMedicamentoDosagemDia))
            {
                DispenserStatus.QuantidadeTotalMedicamentoDosagemDia = quantidadeTotalMedicamentoDosagemDia;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["QuantidadeTotalMedicamentosTreatment"].ToString(), out int quantidadeTotalMedicamentosTreatment))
            {
                DispenserStatus.QuantidadeTotalMedicamentosTreatment = quantidadeTotalMedicamentosTreatment;
            }
            else
            {
                // Tratar erro de conversão
            }

           
             if (int.TryParse(reader["QuantidadeMedicamentoSemanal"].ToString(), out int quantidadeMedicamentoSemanal))
            {
                DispenserStatus.QuantidadeMedicamentoSemanal = quantidadeMedicamentoSemanal;
            }
            else
            {
                // Tratar erro de conversão
            }

            if (int.TryParse(reader["QuantidadeAtualMedicamentoCaixaTreatment"].ToString(), out int quantidadeAtualMedicamentoCaixaTreatment))
            {
                DispenserStatus.QuantidadeAtualMedicamentoCaixaTreatment = quantidadeAtualMedicamentoCaixaTreatment;
            }
            else
            {
                // Tratar erro de conversão
            }

           
            if (int.TryParse(reader["QuantidadeMedicamentoFaltanteParaFimTreatment"].ToString(), out int quantidadeMedicamentoFaltanteParaFimTreatment))
            {
                DispenserStatus.QuantidadeMedicamentoFaltanteParaFimTreatment = quantidadeMedicamentoFaltanteParaFimTreatment;
            }
            else
            {
                // Tratar erro de conversão
            }
             
            if (int.TryParse(reader["QuantidadeDiasFaltanteParaFimTreatment"].ToString(), out int quantidadeDiasFaltanteParaFimTreatment))
            {
                DispenserStatus.QuantidadeDiasFaltanteParaFimTreatment = quantidadeDiasFaltanteParaFimTreatment;
            }
            else
            {
                // Tratar erro de conversão
            }

            DispenserStatus.DataCriacao = reader["DataCriacao"].ToString();
            DispenserStatus.DataAtualizacaoSemanal = reader["DataAtualizacaoSemanal"].ToString();
            DispenserStatus.DataInicioTreatment = reader["DataInicioTreatment"].ToString();
            DispenserStatus.DataFinalPrevistoTreatment = reader["DataFinalPrevistoTreatment"].ToString();
            DispenserStatus.DataFinalMarcadoTreatment = reader["DataFinalMarcadoTreatment"].ToString();

            if (int.TryParse(reader["StatusTreatment"].ToString(), out int statusTreatment))
            {
                DispenserStatus.StatusTreatment = statusTreatment;
            }
            else
            {
                // Tratar erro de conversão
            }
            return DispenserStatus;
        }
    }
}
