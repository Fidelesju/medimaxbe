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
                WHERE esta_ativo = 1
                AND horario_inicio BETWEEN '{startTime}' AND '{finishTime}'
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
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
