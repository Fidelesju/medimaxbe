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

        public async Task<List<TreatmentResponseModel>> GetTreatmentByName(string name)
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                SELECT 
                  t.id AS Id,
                  t.nome_medication AS Name,
                  t.quantidade_medications AS MedicineQuantity,
                  t.horario_inicio AS StartTime,
                  t.intervalo_Treatment AS TreatmentInterval,
                  t.tempo_Treatment_dias AS TreatmentDurationDays,
                  t.recomendacoes_alimentacao AS DietaryRecommendations,
                  t.observacao AS Observation,
                  t.esta_ativo AS IsActive
               FROM Treatment t
               WHERE t.nome_medication = '{name}'
               AND esta_ativo = 1
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }


        protected override TreatmentResponseModel Mapper(DbDataReader reader)
        {
            TreatmentResponseModel Treatment;
            Treatment = new TreatmentResponseModel();
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
