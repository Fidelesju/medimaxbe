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
                         treatment_id AS TreatmentId,
                         medication_id AS MedicationId,
                         total_quantity_medication_box AS TotalQuantityMedicationBox,
                         total_quantity_box_treatment AS TotalQuantityBoxTreatment,
                         total_quantity_medication_dosage_day AS TotalQuantityMedicationDosageDay,
                         total_quantity_medicamentos_treatment AS TotalQuantityMedicationsTreatment,
                         treatment_interval_hours AS TreatmentIntervalHours,
                         treatment_interval_days AS TreatmentIntervalDays,
                         quantity_medicine_per_dosage AS QuantityMedicinePerDosage,
                         daily_dosage_frequency AS DailyDosageFrequency,
                         weekly_medication_quantity AS WeeklyMedicationQuantity,
                         current_quantity_medication_box_treatment AS CurrentQuantityMedicationBoxTreatment,
                         missing_medicine_quantity_to_end_treatment AS MissingMedicineQuantityToEndTreatment,
                         quantity_days_missing_to_end_treatment AS QuantityDaysMissingToEndTreatment,
                         creation_data AS CreationDate,
                         weekly_update_date AS WeeklyUpdateDate,
                         treatment_start_date AS TreatmentStartDate,
                         final_date_expected_treatment AS FinalDateExpectedTreatment,
                         final_date_marked_treatment AS FinalDateMarkedTreatment,
                         treatment_status AS TreatmentStatus
                    FROM status_dispenser
                    WHERE treatment_id = {TreatmentId}
                    ORDER BY id DESC
                    LIMIT 1;
                ";
            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultObject();
            await Disconnect();
            return medicamentoLista;
        }


        protected override DispenserStatusResponseModel Mapper ( DbDataReader reader )
        {
            DispenserStatusResponseModel dispenserStatus = new DispenserStatusResponseModel();

            // Helper function for parsing integers with error handling
            int TryParseInt ( string value )
            {
                return int.TryParse(value, out int result) ? result : default(int);
            }

            // Mapping the fields
            dispenserStatus.Id = TryParseInt(reader["Id"].ToString());
            dispenserStatus.TreatmentId = TryParseInt(reader["TreatmentId"].ToString());
            dispenserStatus.MedicamentoId = TryParseInt(reader["MedicationId"].ToString());
            dispenserStatus.QuantidadeTotalMedicamentoCaixa = TryParseInt(reader["TotalQuantityMedicationBox"].ToString());
            dispenserStatus.QuantidadeTotalCaixaTreatment = TryParseInt(reader["TotalQuantityBoxTreatment"].ToString());
            dispenserStatus.IntervaloTreatmentHoras = TryParseInt(reader["TreatmentIntervalHours"].ToString());
            dispenserStatus.IntervaloTreatmentDias = TryParseInt(reader["TreatmentIntervalDays"].ToString());
            dispenserStatus.QuantidadeMedicamentoPorDosagem = TryParseInt(reader["QuantityMedicinePerDosage"].ToString());
            dispenserStatus.FrenqueciaDosagemDiaria = TryParseInt(reader["DailyDosageFrequency"].ToString());
            dispenserStatus.QuantidadeTotalMedicamentoDosagemDia = TryParseInt(reader["TotalQuantityMedicationDosageDay"].ToString());
            dispenserStatus.QuantidadeTotalMedicamentosTreatment = TryParseInt(reader["TotalQuantityMedicationsTreatment"].ToString());
            dispenserStatus.QuantidadeMedicamentoSemanal = TryParseInt(reader["WeeklyMedicationQuantity"].ToString());
            dispenserStatus.QuantidadeAtualMedicamentoCaixaTreatment = TryParseInt(reader["CurrentQuantityMedicationBoxTreatment"].ToString());
            dispenserStatus.QuantidadeMedicamentoFaltanteParaFimTreatment = TryParseInt(reader["MissingMedicineQuantityToEndTreatment"].ToString());
            dispenserStatus.QuantidadeDiasFaltanteParaFimTreatment = TryParseInt(reader["QuantityDaysMissingToEndTreatment"].ToString());

            dispenserStatus.DataCriacao = reader["CreationDate"].ToString();
            dispenserStatus.DataAtualizacaoSemanal = reader["WeeklyUpdateDate"].ToString();
            dispenserStatus.DataInicioTreatment = reader["TreatmentStartDate"].ToString();
            dispenserStatus.DataFinalPrevistoTreatment = reader["FinalDateExpectedTreatment"].ToString();
            dispenserStatus.DataFinalMarcadoTreatment = reader["FinalDateMarkedTreatment"].ToString();

            dispenserStatus.StatusTreatment = TryParseInt(reader["TreatmentStatus"].ToString());

            return dispenserStatus;
        }

    }
}
