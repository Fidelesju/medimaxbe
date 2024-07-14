using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class TreatmentDb : Db<TreatmentResponseModel>, ITreatmentDb
    {
        public TreatmentDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

       
        public async Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId(int medicineId, int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                     SELECT 
                        t.id AS Id,
                        t.remedio_id AS MedicineId,
                        t.nome_medication AS Name,
                        t.quantidade_medications AS MedicineQuantity,
                        t.horario_inicio AS StartTime,
                        t.intervalo_Treatment AS TreatmentInterval,
                        t.tempo_Treatment_dias AS TreatmentDurationDays,
                        t.recomendacoes_alimentacao AS DietaryRecommendations,
                        t.observacao AS Observation,
                        t.esta_ativo AS IsActive
                    FROM Treatment t
                    INNER JOIN medicamentos m ON m.id = t.remedio_id 
                    WHERE m.id = {medicineId}
                    AND m.esta_ativo = 1
                    AND t.esta_ativo = 1
                    AND m.UserId = {userId};
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        } 
        
        public async Task<List<TreatmentResponseModel>> BuscarHorarioTreatmentPorNome(int treatmentId, int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
             SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medication AS Name,
                t.quantidade_medications AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_Treatment AS TreatmentInterval,
                t.tempo_Treatment_dias AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem as DosageTime 
                FROM horarios_dosagem hd
                INNER JOIN Treatment t ON t.id = hd.Treatment_id 
                INNER JOIN medicamentos m ON m.id = t.remedio_id 
                WHERE hd.Treatment_id = {treatmentId}
                AND m.esta_ativo = 1
                AND t.esta_ativo = 1
                AND m.UserId = {userId}
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }
        
        public async Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId )
        {
            string sql;
            TreatmentResponseModel treatment;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medication AS Name,
                t.quantidade_medications AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_Treatment AS TreatmentInterval,
                t.tempo_Treatment_dias AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem AS DosageTime
            FROM Treatment t
            INNER JOIN (
                SELECT 
                    Treatment_id, 
                    MIN(horario_dosagem) AS horario_dosagem
                FROM horarios_dosagem
                GROUP BY Treatment_id
            ) hd ON hd.Treatment_id = t.id
            INNER JOIN medicamentos m ON t.remedio_id = m.id
            WHERE t.esta_ativo = 1
            AND m.esta_ativo = 1
            AND t.id = {treatmentId}
            AND m.UserId = {userId}
                ";

            await Connect();
            await Query(sql);
            treatment = await GetQueryResultObject();
            await Disconnect();
            return treatment;
        }
        
        public async Task<TreatmentResponseModel> BuscarTreatmentPorIdParaStatus ( int treatmentId, int userId )
        {
            string sql;
            TreatmentResponseModel treatment;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medication AS Name,
                t.quantidade_medications AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_Treatment AS TreatmentInterval,
                COALESCE(sd.quantidade_dias_faltante_para_fim_Treatment, t.tempo_Treatment_dias) AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem AS DosageTime
            FROM Treatment t
            INNER JOIN (
                SELECT 
                    Treatment_id, 
                    MIN(horario_dosagem) AS horario_dosagem
                FROM horarios_dosagem
                GROUP BY Treatment_id
            ) hd ON hd.Treatment_id = t.id
            INNER JOIN medicamentos m ON t.remedio_id = m.id
            LEFT JOIN status_dispenser sd ON t.id = sd.Treatment_id
            WHERE t.esta_ativo = 1
            AND m.esta_ativo = 1
            AND t.id = {treatmentId}
            AND m.UserId = {userId}
            ORDER BY id DESC
            LIMIT 1

                ";

            await Connect();
            await Query(sql);
            treatment = await GetQueryResultObject();
            await Disconnect();
            return treatment;
        }

        public async Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medication AS Name,
                t.quantidade_medications AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_Treatment AS TreatmentInterval,
                t.tempo_Treatment_dias AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem AS DosageTime
            FROM Treatment t
            INNER JOIN (
                SELECT 
                    Treatment_id, 
                    MIN(horario_dosagem) AS horario_dosagem
                FROM horarios_dosagem
                GROUP BY Treatment_id
            ) hd ON hd.Treatment_id = t.id
            INNER JOIN medicamentos m ON t.remedio_id = m.id
            WHERE t.esta_ativo = 1
            AND m.esta_ativo = 1
            AND m.UserId = {userId}
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }
        public async Task<List<TreatmentResponseModel>> BuscarTodosTreatmentInativos ( int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                  SELECT 
                    t.id AS Id,
                    t.remedio_id AS MedicineId,
                    t.nome_medication AS Name,
                    t.quantidade_medications AS MedicineQuantity,
                    t.horario_inicio AS StartTime,
                    t.intervalo_Treatment AS TreatmentInterval,
                    t.tempo_Treatment_dias AS TreatmentDurationDays,
                    t.recomendacoes_alimentacao AS DietaryRecommendations,
                    t.observacao AS Observation,
                    t.esta_ativo AS IsActive,
                    hd.horario_dosagem AS DosageTime
                FROM Treatment t
                INNER JOIN (
                    SELECT 
                        Treatment_id, 
                        MIN(horario_dosagem) AS horario_dosagem
                    FROM horarios_dosagem
                    GROUP BY Treatment_id
                ) hd ON hd.Treatment_id = t.id
                INNER JOIN medicamentos m ON t.remedio_id = m.id
                WHERE t.esta_ativo = 0
                AND m.esta_ativo = 1
                AND m.UserId = {userId}
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        public async Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                  SELECT 
                    t.id AS Id,
                    t.remedio_id AS MedicineId,
                    t.nome_medication AS Name,
                    t.quantidade_medications AS MedicineQuantity,
                    t.horario_inicio AS StartTime,
                    t.intervalo_Treatment AS TreatmentInterval,
                    t.tempo_Treatment_dias AS TreatmentDurationDays,
                    t.recomendacoes_alimentacao AS DietaryRecommendations,
                    t.observacao AS Observation,
                    t.esta_ativo AS IsActive,
                    hd.horario_dosagem AS DosageTime
                 FROM Treatment t
                 INNER JOIN (
                    SELECT 
	                    Treatment_id, 
	                    horario_dosagem AS horario_dosagem
                    FROM horarios_dosagem hd 
                    WHERE hd.horario_dosagem BETWEEN '{startTime}' AND '{finishTime}'
                    GROUP BY Treatment_id, horario_dosagem
                    ) hd ON hd.Treatment_id = t.id
                INNER JOIN medicamentos m ON t.remedio_id = m.id
                WHERE t.esta_ativo = 1
                AND m.esta_ativo = 1
                AND m.UserId = {userId}
                AND NOT EXISTS (
                    SELECT 1
                    FROM gerenciamento_Treatment gt
                    WHERE gt.Treatment_id = t.id
                    AND gt.horario_correto_Treatment BETWEEN '{startTime}' AND '{finishTime}'
                    AND gt.data_ingestao_medication = now()
                    )
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        } 

        public async Task<bool> AlterandoTreatment(int remedio_id,
            string nome,
            int quantidade_medications,
            string horario_inicio,
            int intervalo_Treatment,
            int tempo_Treatment_dias,
            string recomendacoes_alimentacao,
            string observacao,
            int id)
        {
            string sql;
            bool success;
            sql = $@"
                UPDATE Treatment t
                SET t.nome_medication = '{nome}', 
                t.quantidade_medications = '{quantidade_medications}', 
                t.horario_inicio = '{horario_inicio}', 
                t.intervalo_Treatment = {intervalo_Treatment}, 
                t.tempo_Treatment_dias = {tempo_Treatment_dias}, 
                t.recomendacoes_alimentacao = '{recomendacoes_alimentacao}', 
                t.observacao = '{observacao}' 
                WHERE t.id = {id}
                ";

            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        public async Task<bool> DeleteTreatment(int id)
        {
            string sql;
            bool success;
            sql = $@"
               UPDATE Treatment t
                SET t.esta_ativo = 0
               WHERE t.id = {id}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        protected override TreatmentResponseModel Mapper(DbDataReader reader)
        {
            TreatmentResponseModel treatment = new TreatmentResponseModel();

            treatment.Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id"));
            treatment.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"));
            treatment.MedicineQuantity = reader.IsDBNull(reader.GetOrdinal("MedicineQuantity")) ? null : reader.GetInt32(reader.GetOrdinal("MedicineQuantity"));
            treatment.MedicineId = reader.IsDBNull(reader.GetOrdinal("MedicineId")) ? null : reader.GetInt32(reader.GetOrdinal("MedicineId"));
            treatment.StartTime = reader.IsDBNull(reader.GetOrdinal("StartTime")) ? null : reader.GetString(reader.GetOrdinal("StartTime"));
            treatment.DietaryRecommendations = reader.IsDBNull(reader.GetOrdinal("DietaryRecommendations")) ? null : reader.GetString(reader.GetOrdinal("DietaryRecommendations"));
            treatment.Observation = reader.IsDBNull(reader.GetOrdinal("Observation")) ? null : reader.GetString(reader.GetOrdinal("Observation"));
            treatment.TreatmentInterval = reader.IsDBNull(reader.GetOrdinal("TreatmentInterval")) ? null : reader.GetInt32(reader.GetOrdinal("TreatmentInterval"));
            treatment.TreatmentDurationDays = reader.IsDBNull(reader.GetOrdinal("TreatmentDurationDays")) ? null : reader.GetInt32(reader.GetOrdinal("TreatmentDurationDays"));
            treatment.IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? null : reader.GetInt32(reader.GetOrdinal("IsActive"));

            return treatment;
        }

    }
}
