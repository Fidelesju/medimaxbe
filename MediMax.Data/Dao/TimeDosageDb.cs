using System.Data.Common;
using AutoMapper;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class TimeDosageDb : Db<TimeDosageResponseModel>, ITimeDosageDb
    {
        private IMapper _mapper;
        public TimeDosageDb(
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<TimeDosageResponseModel> BuscarHorarioDosagemExistente(int Treatment_id, string horario_dosagem)
        {
            string sql;
            TimeDosageResponseModel horarioDosagemResponseModel;
            sql = $@"
                SELECT 
                     td.id as Id,
                     td.treatment_id as TreatmentId,
                     td.time as HorarioDosagem
                 FROM time_dosage td
                 WHERE td.treatment_id = {Treatment_id}
                 AND td.time = '{horario_dosagem}'
                ";

            await Connect();
            await Query(sql);
            horarioDosagemResponseModel = await GetQueryResultObject();
            await Disconnect();
            return horarioDosagemResponseModel;
        } 
        
        public async Task<List<TimeDosageResponseModel>> GetDosageTimeByTreatmentId(int treatment_id)
        {
            string sql;
            List<TimeDosageResponseModel> horarioDosagemResponseModel;
            sql = $@"
                SELECT 
                    td.id AS Id,
                    td.Treatment_id AS TreatmentId,
                    td.time AS TimeDosage,
                    td.treatment_user_id AS UserId
                FROM time_dosage td
                WHERE td.treatment_id = {treatment_id}
                ";

            await Connect();
            await Query(sql);
            horarioDosagemResponseModel = await GetQueryResultList();
            await Disconnect();
            return horarioDosagemResponseModel;
        }

        public async Task<List<TimeDosageResponseModel>> GetDosageTimeByUserIdAndTime ( int userId, string time )
        {
            string sql;
            List<TimeDosageResponseModel> horarioDosagemResponseModel;
            sql = $@"
                SELECT 
                     td.id AS Id,
                     td.time AS TimeDosage,
                     td.treatment_id AS TreatmentId,
                     td.treatment_user_id AS UserId
                FROM time_dosage td
                WHERE td.treatment_user_id = {userId}
                AND td.time = '{time}';
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
                DELETE FROM time_dosage td WHERE td.treatment_id = {Treatment_id}
            ";

            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        protected override TimeDosageResponseModel Mapper(DbDataReader reader)
        {
            return _mapper.Map<TimeDosageResponseModel>(reader);
        }
    }
}
