using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class HorariosDosagemDb : Db<HorariosDosagemResponseModel>, IHorariosDosagemDb
    {
        public HorariosDosagemDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<HorariosDosagemResponseModel> BuscarHorarioDosagemExistente(int Treatment_id, string horario_dosagem)
        {
            string sql;
            HorariosDosagemResponseModel horarioDosagemResponseModel;
            sql = $@"
                SELECT 
                    hd.id as Id,
	                hd.tratamento_id as TreatmentId,
                    hd.horario_dosagem as HorarioDosagem
                FROM horarios_dosagem hd 
                WHERE hd.tratamento_id = {Treatment_id}
                AND horario_dosagem = '{horario_dosagem}'
                ";

            await Connect();
            await Query(sql);
            horarioDosagemResponseModel = await GetQueryResultObject();
            await Disconnect();
            return horarioDosagemResponseModel;
        } 
        
        public async Task<List<HorariosDosagemResponseModel>> GetDosageTimeByTreatmentId(int Treatment_id)
        {
            string sql;
            List<HorariosDosagemResponseModel> horarioDosagemResponseModel;
            sql = $@"
                SELECT 
                    hd.id as Id,
	                hd.Treatment_id as TreatmentId,
                    hd.horario_dosagem as HorarioDosagem
                FROM horarios_dosagem hd 
                WHERE hd.Treatment_id = {Treatment_id}
                ";

            await Connect();
            await Query(sql);
            horarioDosagemResponseModel = await GetQueryResultList();
            await Disconnect();
            return horarioDosagemResponseModel;
        }
      
        public async Task<bool> DeletandoHorarioDosagem(int Treatment_id)
        {
            string sql;
            bool success;
            sql = $@"
                DELETE FROM horarios_dosagem hd WHERE hd.tratamento_id= {Treatment_id}
            ";

            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        protected override HorariosDosagemResponseModel Mapper(DbDataReader reader)
        {
            HorariosDosagemResponseModel horarioDosagem;
            horarioDosagem = new HorariosDosagemResponseModel();
            horarioDosagem.Id = Convert.ToInt32(reader["Id"]);
            horarioDosagem.TreatmentId = Convert.ToInt32(reader["TreatmentId"]);
            horarioDosagem.HorarioDosagem = Convert.ToString(reader["HorarioDosagem"]);
            return horarioDosagem;
        }
    }
}
