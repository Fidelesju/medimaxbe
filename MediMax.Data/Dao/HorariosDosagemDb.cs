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

        public async Task<HorariosDosagemResponseModel> BuscarHorarioDosagemExistente(int tratamento_id, string horario_dosagem)
        {
            string sql;
            HorariosDosagemResponseModel horarioDosagemResponseModel;
            sql = $@"
                SELECT 
                    hd.id as Id,
	                hd.tratamento_id as TratamentoId,
                    hd.horario_dosagem as HorarioDosagem
                FROM horarios_dosagem hd 
                WHERE hd.tratamento_id = {tratamento_id}
                AND horario_dosagem = '{horario_dosagem}'
                ";

            await Connect();
            await Query(sql);
            horarioDosagemResponseModel = await GetQueryResultObject();
            await Disconnect();
            return horarioDosagemResponseModel;
        }
      
        public async Task<bool> DeletandoHorarioDosagem(int tratamento_id)
        {
            string sql;
            bool success;
            sql = $@"
                DELETE FROM horarios_dosagem hd WHERE hd.tratamento_id = {tratamento_id}
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
            horarioDosagem.TratamentoId = Convert.ToInt32(reader["TratamentoId"]);
            horarioDosagem.HorarioDosagem = Convert.ToString(reader["HorarioDosagem"]);
            return horarioDosagem;
        }
    }
}
