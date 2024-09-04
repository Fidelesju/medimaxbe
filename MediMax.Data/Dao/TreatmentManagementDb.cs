using AutoMapper;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using System.Data.Common;

namespace MediMax.Data.Dao
{
    public class TreatmentManagementDb : Db<TreatmentManagementResponseModel>, ITreatmentManagementDbDb
    {
        private IMapper _mapper;
        public TreatmentManagementDb ( 
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext ) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<List<TreatmentManagementResponseModel>> GetAllHistoric ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }

        public async Task<TreatmentManagementResponseModel> BuscarStatusDoUltimoGerenciamento ( int userId )
        {
            string sql;
            TreatmentManagementResponseModel historicoResponseModel;
            sql = $@"
                SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                ORDER BY tm.medication_intake_date DESC
                LIMIT 1
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultObject();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<TreatmentManagementResponseModel> BuscarUltimoGerenciamento ( int userId )
        {
            string sql;
            TreatmentManagementResponseModel historicoResponseModel;
            sql = $@"
                  SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                ORDER BY tm.medication_intake_date DESC
                LIMIT 1
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultObject();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoTomado ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND tm.was_taken = 1
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoNaoTomado ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND tm.was_taken = 0
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico7Dias ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND STR_TO_DATE(tm.medication_intake_date, '%d/%m/%Y') >= now() - INTERVAL 7 DAY
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico15Dias ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND STR_TO_DATE(tm.medication_intake_date, '%d/%m/%Y') >= now() - INTERVAL 15 DAY
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico30Dias ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND STR_TO_DATE(tm.medication_intake_date, '%d/%m/%Y') >= now() - INTERVAL 30 DAY
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico60Dias ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND STR_TO_DATE(tm.medication_intake_date, '%d/%m/%Y') >= now() - INTERVAL 60 DAY
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoUltimoAno ( int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND STR_TO_DATE(tm.medication_intake_date, '%d/%m/%Y') >= now() - INTERVAL 1 YEAR
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }

        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoAnoEspecifico ( string year, int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
               SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND STR_TO_DATE(tm.medication_intake_date, '%d/%m/%Y')  = '{year}'
                ORDER BY tm.medication_intake_date DESC
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoDataEspecifica ( string data, int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                 SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND tm.medication_intake_date = '{data}'
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoPorMedicamento ( string nome, int userId )
        {
            string sql;
            List<TreatmentManagementResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    tm.id as Id,
                    tm.correct_time_treatment as CorrectTimeTreatment,
                    tm.medication_intake_time as MedicationIntakeTime,
                    tm.medication_intake_date as MedicationIntakeDate,
                    tm.was_taken as WasTaken, 
                    tm.treatment_id as TreatmentId,
                    tm.treatment_user_id as UserId,
                    tm.medication_id AS MedicationId,
                    t.name_medication AS MedicationName
                FROM treatment_management tm
                INNER JOIN treatment t ON t.id = tm.treatment_id
                WHERE tm.treatment_user_id = {userId}
                AND t.name_medication  = '%{nome}%'
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }

        protected override TreatmentManagementResponseModel Mapper ( DbDataReader reader )
        {
            return _mapper.Map<TreatmentManagementResponseModel>(reader);
        }
    }
}
