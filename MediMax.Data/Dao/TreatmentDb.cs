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
                  t.nome_medicamento AS Name,
                  t.quantidade_medicamentos AS MedicineQuantity,
                  t.horario_inicio AS StartTime,
                  t.intervalo_tratamento AS TreatmentInterval,
                  t.tempo_tratamento_dias AS TreatmentDurationDays,
                  t.recomendacoes_alimentacao AS DietaryRecommendations,
                  t.observacao AS Observation,
                  t.esta_ativo AS IsActive
               FROM tratamento t
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
                AND t.horario_inicio BETWEEN '16:41' AND '17:41'
                AND NOT EXISTS (
                    SELECT 1
                    FROM gerenciamento_tratamento gt
                    WHERE gt.remedio_id = t.medicamento_id
                    AND gt.horario_correto_tratamento  BETWEEN '16:41' AND '17:41'
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

        protected override TratamentoResponseModel Mapper(DbDataReader reader)
        {
            TratamentoResponseModel Treatment;
            Treatment = new TratamentoResponseModel();
            Treatment.Id = Convert.ToInt32(reader["Id"]);
            Treatment.Name = Convert.ToString(reader["Name"]);
            Treatment.MedicineQuantity = Convert.ToInt32(reader["MedicineQuantity"]);
            Treatment.StartTime = Convert.ToString(reader["StartTime"]);
            Treatment.DietaryRecommendations = Convert.ToString(reader["DietaryRecommendations"]);
            Treatment.Observation = Convert.ToString(reader["Observation"]);
            Treatment.TreatmentInterval = Convert.ToInt32(reader["TreatmentInterval"]);
            Treatment.TreatmentDurationDays = Convert.ToInt32(reader["TreatmentDurationDays"]);
            Treatment.IsActive = Convert.ToInt32(reader["IsActive"]);
            return Treatment;
        }
    }
}
