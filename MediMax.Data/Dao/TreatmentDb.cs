using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class TreatmentDb : Db<TratamentoResponseModel>, ITratamentoDb
    {
        public TreatmentDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name)
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
               WHERE t.nome_medicamento = '{name}'
               AND esta_ativo = 1
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        public async Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime)
        {
            string sql;
            List<TratamentoResponseModel> TreatmentList;
            sql = $@"
                SELECT 
                   t.id AS Id,
                   t.nome_medicamento AS Name,
                   t.quantidade_medicamentos AS MedicineQuantity,
                   t.horario_inicio AS StartTime,
                   t.intervalo_tratamento AS TreatmentInterval,
                   t.tempo_tratamento_dias AS TreatmentDurationDays,
                   t.recomendacoes_alimentacao AS DietaryRecommendations,
                   t.observacao AS Observation,
                   t.esta_ativo AS IsActive
                FROM tratamento t
                WHERE t.esta_ativo = 1
                AND t.horario_inicio BETWEEN '{startTime}' AND {finishTime}'
                AND NOT EXISTS (
                    SELECT 1
                    FROM gerenciamento_tratamento gt
                    WHERE gt.remedio_id = t.medicamento_id
                    AND gt.horario_correto_tratamento  BETWEEN '{startTime}' AND '{finishTime}'
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
