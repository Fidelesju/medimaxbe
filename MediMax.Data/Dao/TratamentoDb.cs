using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class TratamentoDb : Db<TratamentoResponseModel>, ITratamentoDb
    {
        public TratamentoDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

       
        public async Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name, int userId )
        {
            string sql;
            List<TratamentoResponseModel> TreatmentList;
            sql = $@"
              SELECT 
                   t.id AS Id,
                   t.remedio_id AS MedicineId,
                   t.nome_medicamento AS Name,
                   t.quantidade_medicamentos AS MedicineQuantity,
                   t.horario_inicio AS StartTime,
                   t.intervalo_tratamento AS TreatmentInterval,
                   t.tempo_tratamento_dias AS TreatmentDurationDays,
                   t.recomendacoes_alimentacao AS DietaryRecommendations,
                   t.observacao AS Observation,
                   t.esta_ativo AS IsActive,
                   hd.horario_dosagem as DosageTime
               FROM tratamento t
               INNER JOIN horarios_dosagem hd ON hd.tratamento_id = t.id
               INNER JOIN medicamentos m ON t.remedio_id = m.id
               WHERE t.nome_medicamento = '{name}'
               AND t.esta_ativo = 1
               AND m.esta_ativo = 1
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }
        
        public async Task<TratamentoResponseModel> BuscarTratamentoPorId ( int treatmentId, int userId )
        {
            string sql;
            TratamentoResponseModel treatment;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medicamento AS Name,
                t.quantidade_medicamentos AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_tratamento AS TreatmentInterval,
                t.tempo_tratamento_dias AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem AS DosageTime
            FROM tratamento t
            INNER JOIN (
                SELECT 
                    tratamento_id, 
                    MIN(horario_dosagem) AS horario_dosagem
                FROM horarios_dosagem
                GROUP BY tratamento_id
            ) hd ON hd.tratamento_id = t.id
            INNER JOIN medicamentos m ON t.remedio_id = m.id
            WHERE t.esta_ativo = 1
            AND m.esta_ativo = 1
            AND t.id = {treatmentId}
            AND m.usuarioId = {userId}
                ";

            await Connect();
            await Query(sql);
            treatment = await GetQueryResultObject();
            await Disconnect();
            return treatment;
        }
        
        public async Task<TratamentoResponseModel> BuscarTratamentoPorIdParaStatus ( int treatmentId, int userId )
        {
            string sql;
            TratamentoResponseModel treatment;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medicamento AS Name,
                t.quantidade_medicamentos AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_tratamento AS TreatmentInterval,
                COALESCE(sd.quantidade_dias_faltante_para_fim_tratamento, t.tempo_tratamento_dias) AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem AS DosageTime
            FROM tratamento t
            INNER JOIN (
                SELECT 
                    tratamento_id, 
                    MIN(horario_dosagem) AS horario_dosagem
                FROM horarios_dosagem
                GROUP BY tratamento_id
            ) hd ON hd.tratamento_id = t.id
            INNER JOIN medicamentos m ON t.remedio_id = m.id
            LEFT JOIN status_dispenser sd ON t.id = sd.tratamento_id
            WHERE t.esta_ativo = 1
            AND m.esta_ativo = 1
            AND t.id = {treatmentId}
            AND m.usuarioId = {userId}
            ORDER BY id DESC
            LIMIT 1

                ";

            await Connect();
            await Query(sql);
            treatment = await GetQueryResultObject();
            await Disconnect();
            return treatment;
        }

        public async Task<List<TratamentoResponseModel>> BuscarTodosTratamentoAtivos ( int userId )
        {
            string sql;
            List<TratamentoResponseModel> TreatmentList;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.remedio_id AS MedicineId,
                t.nome_medicamento AS Name,
                t.quantidade_medicamentos AS MedicineQuantity,
                t.horario_inicio AS StartTime,
                t.intervalo_tratamento AS TreatmentInterval,
                t.tempo_tratamento_dias AS TreatmentDurationDays,
                t.recomendacoes_alimentacao AS DietaryRecommendations,
                t.observacao AS Observation,
                t.esta_ativo AS IsActive,
                hd.horario_dosagem AS DosageTime
            FROM tratamento t
            INNER JOIN (
                SELECT 
                    tratamento_id, 
                    MIN(horario_dosagem) AS horario_dosagem
                FROM horarios_dosagem
                GROUP BY tratamento_id
            ) hd ON hd.tratamento_id = t.id
            INNER JOIN medicamentos m ON t.remedio_id = m.id
            WHERE t.esta_ativo = 1
            AND m.esta_ativo = 1
            AND m.usuarioId = {userId}
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }
        public async Task<List<TratamentoResponseModel>> BuscarTodosTratamentoInativos ( int userId )
        {
            string sql;
            List<TratamentoResponseModel> TreatmentList;
            sql = $@"
                  SELECT 
                    t.id AS Id,
                    t.remedio_id AS MedicineId,
                    t.nome_medicamento AS Name,
                    t.quantidade_medicamentos AS MedicineQuantity,
                    t.horario_inicio AS StartTime,
                    t.intervalo_tratamento AS TreatmentInterval,
                    t.tempo_tratamento_dias AS TreatmentDurationDays,
                    t.recomendacoes_alimentacao AS DietaryRecommendations,
                    t.observacao AS Observation,
                    t.esta_ativo AS IsActive,
                    hd.horario_dosagem AS DosageTime
                FROM tratamento t
                INNER JOIN (
                    SELECT 
                        tratamento_id, 
                        MIN(horario_dosagem) AS horario_dosagem
                    FROM horarios_dosagem
                    GROUP BY tratamento_id
                ) hd ON hd.tratamento_id = t.id
                INNER JOIN medicamentos m ON t.remedio_id = m.id
                WHERE t.esta_ativo = 0
                AND m.esta_ativo = 1
                AND m.usuarioId = {userId}
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        public async Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime, int userId )
        {
            string sql;
            List<TratamentoResponseModel> TreatmentList;
            sql = $@"
                  SELECT 
                    t.id AS Id,
                    t.remedio_id AS MedicineId,
                    t.nome_medicamento AS Name,
                    t.quantidade_medicamentos AS MedicineQuantity,
                    t.horario_inicio AS StartTime,
                    t.intervalo_tratamento AS TreatmentInterval,
                    t.tempo_tratamento_dias AS TreatmentDurationDays,
                    t.recomendacoes_alimentacao AS DietaryRecommendations,
                    t.observacao AS Observation,
                    t.esta_ativo AS IsActive,
                    hd.horario_dosagem AS DosageTime
                 FROM tratamento t
                 INNER JOIN (
                    SELECT 
	                    tratamento_id, 
	                    horario_dosagem AS horario_dosagem
                    FROM horarios_dosagem hd 
                    WHERE hd.horario_dosagem BETWEEN '{startTime}' AND '{finishTime}'
                    GROUP BY tratamento_id, horario_dosagem
                    ) hd ON hd.tratamento_id = t.id
                INNER JOIN medicamentos m ON t.remedio_id = m.id
                WHERE t.esta_ativo = 1
                AND m.esta_ativo = 1
                AND m.usuarioId = {userId}
                AND NOT EXISTS (
                    SELECT 1
                    FROM gerenciamento_tratamento gt
                    WHERE gt.tratamento_id = t.id
                    AND gt.horario_correto_tratamento BETWEEN '{startTime}' AND '{finishTime}'
                    AND gt.data_ingestao_medicamento = now()
                    )
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        } 

        public async Task<bool> AlterandoTratamento(int remedio_id,
            string nome,
            int quantidade_medicamentos,
            string horario_inicio,
            int intervalo_tratamento,
            int tempo_tratamento_dias,
            string recomendacoes_alimentacao,
            string observacao,
            int id)
        {
            string sql;
            bool success;
            sql = $@"
                UPDATE tratamento t
                SET t.nome_medicamento = '{nome}', 
                t.quantidade_medicamentos = '{quantidade_medicamentos}', 
                t.horario_inicio = '{horario_inicio}', 
                t.intervalo_tratamento = {intervalo_tratamento}, 
                t.tempo_tratamento_dias = {tempo_tratamento_dias}, 
                t.recomendacoes_alimentacao = '{recomendacoes_alimentacao}', 
                t.observacao = '{observacao}' 
                WHERE t.id = {id}
                ";

            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        public async Task<bool> DeletandoTratamento(int id)
        {
            string sql;
            bool success;
            sql = $@"
               UPDATE tratamento t
                SET t.esta_ativo = 0
                WHERE t.id = {id}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        protected override TratamentoResponseModel Mapper(DbDataReader reader)
        {
            TratamentoResponseModel treatment = new TratamentoResponseModel();

            treatment.Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id"));
            treatment.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"));
            treatment.MedicineQuantity = reader.IsDBNull(reader.GetOrdinal("MedicineQuantity")) ? null : reader.GetInt32(reader.GetOrdinal("MedicineQuantity"));
            treatment.MedicineId = reader.IsDBNull(reader.GetOrdinal("MedicineId")) ? null : reader.GetInt32(reader.GetOrdinal("MedicineId"));
            treatment.StartTime = reader.IsDBNull(reader.GetOrdinal("StartTime")) ? null : reader.GetString(reader.GetOrdinal("StartTime"));
            treatment.DietaryRecommendations = reader.IsDBNull(reader.GetOrdinal("DietaryRecommendations")) ? null : reader.GetString(reader.GetOrdinal("DietaryRecommendations"));
            treatment.Observation = reader.IsDBNull(reader.GetOrdinal("Observation")) ? null : reader.GetString(reader.GetOrdinal("Observation"));

            // Tratamento para DosageTime: Converter string para List<string>
            if (!reader.IsDBNull(reader.GetOrdinal("DosageTime")))
            {
                string dosageTime = reader.GetString(reader.GetOrdinal("DosageTime"));
                // Supondo que a string seja uma lista de strings separadas por vírgulas
                treatment.DosageTime = dosageTime.Split(',').ToList();
            }
            else
            {
                treatment.DosageTime = new List<string>();
            }

            treatment.TreatmentInterval = reader.IsDBNull(reader.GetOrdinal("TreatmentInterval")) ? null : reader.GetInt32(reader.GetOrdinal("TreatmentInterval"));
            treatment.TreatmentDurationDays = reader.IsDBNull(reader.GetOrdinal("TreatmentDurationDays")) ? null : reader.GetInt32(reader.GetOrdinal("TreatmentDurationDays"));
            treatment.IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? null : reader.GetInt32(reader.GetOrdinal("IsActive"));

            return treatment;
        }

    }
}
