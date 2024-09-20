using AutoMapper;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using System.Data.Common;

namespace MediMax.Data.Dao
{
    public class TreatmentDb : Db<TreatmentResponseModel>, ITreatmentDb
    {
        private IMapper _mapper;

        public TreatmentDb ( 
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            MediMaxDbContext dbContext ) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId ( int medicineId, int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                     SELECT 
                        t.id AS Id,
                        t.medication_id AS MedicineId,
                        t.name_medication AS NameMedication,
                        t.medication_quantity AS MedicineQuantity,
                        t.start_time AS StartTime,
                        t.treatment_interval_hours AS TreatmentIntervalHours,
                        t.treatment_interval_days AS TreatmentDurationDays,
                        t.dietary_recommendations AS DietaryRecommendations,
                        t.observation AS Observation,
                        t.is_active AS IsActive,
                        t.continuous_use AS ContinuousUse,
                        t.medication_Id AS MedicationId,
                        t.user_id AS UserId
                    FROM treatment t
                    WHERE t.medication_id = {medicineId}
                    AND t.is_active = 1
                    AND t.user_id = {userId};
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        public async Task<List<TreatmentResponseModel>> GetTreatmentByMedicationName ( int medicineId, int userId , string name)
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                    SELECT 
                        t.id AS Id,
                        t.medication_id AS MedicineId,
                        t.name_medication AS NameMedication,
                        t.medication_quantity AS MedicineQuantity,
                        t.start_time AS StartTime,
                        t.treatment_interval_hours AS TreatmentIntervalHours,
                        t.treatment_interval_days AS TreatmentDurationDays,
                        t.dietary_recommendations AS DietaryRecommendations,
                        t.observation AS Observation,
                        t.is_active AS IsActive,
                        t.continuous_use AS ContinuousUse,
                        t.medication_Id AS MedicationId,
                        t.user_id AS UserId
                    FROM treatment t
                    WHERE t.medication_id = {medicineId}
                    AND t.is_active = 1
                    AND t.user_id = {userId}
                    AND t.name_medication like '%{name}%';
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        public async Task<List<TreatmentResponseModel>> GetListTreatmentById ( int treatmentId, int userId )
        {
            string sql;
            List<TreatmentResponseModel> treatment;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.medication_id AS MedicineId,
                t.name_medication AS NameMedication,
                t.medication_quantity AS MedicineQuantity,
                t.start_time AS StartTime,
                t.treatment_interval_hours AS TreatmentIntervalHours,
                t.treatment_interval_days AS TreatmentDurationDays,
                t.dietary_recommendations AS DietaryRecommendations,
                t.observation AS Observation,
                t.is_active AS IsActive,
                t.continuous_use AS ContinuousUse,
                t.medication_Id AS MedicationId,
                t.user_id AS UserId
            FROM treatment t
            WHERE t.id = {treatmentId}
            AND t.is_active = 1
            AND t.user_id = {userId};
                ";

            await Connect();
            await Query(sql);
            treatment = await GetQueryResultList();
            await Disconnect();
            return treatment;
        }
        public async Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId )
        {
            string sql;
            TreatmentResponseModel treatment;
            sql = $@"
              SELECT 
                t.id AS Id,
                t.medication_id AS MedicineId,
                t.name_medication AS NameMedication,
                t.medication_quantity AS MedicineQuantity,
                t.start_time AS StartTime,
                t.treatment_interval_hours AS TreatmentIntervalHours,
                t.treatment_interval_days AS TreatmentDurationDays,
                t.dietary_recommendations AS DietaryRecommendations,
                t.observation AS Observation,
                t.is_active AS IsActive,
                t.continuous_use AS ContinuousUse,
                t.medication_Id AS MedicationId,
                t.user_id AS UserId
            FROM treatment t
            WHERE t.id = {treatmentId}
            AND t.is_active = 1
            AND t.user_id = {userId};
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
                    t.medication_id AS MedicineId,
                    t.name_medication AS NameMedication,
                    t.medication_quantity AS MedicineQuantity,
                    t.start_time AS StartTime,
                    t.treatment_interval_hours AS TreatmentIntervalHours,
                    t.treatment_interval_days AS TreatmentDurationDays,
                    t.dietary_recommendations AS DietaryRecommendations,
                    t.observation AS Observation,
                    t.is_active AS IsActive,
                    t.continuous_use AS ContinuousUse,
                    t.medication_Id AS MedicationId,
                    t.user_id AS UserId
                FROM treatment t
                WHERE t.id = {treatmentId}
                AND t.is_active = 1
                AND t.user_id = {userId};
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
                    t.medication_id AS MedicineId,
                    t.name_medication AS NameMedication,
                    t.medication_quantity AS MedicineQuantity,
                    t.start_time AS StartTime,
                    t.treatment_interval_hours AS TreatmentIntervalHours,
                    t.treatment_interval_days AS TreatmentDurationDays,
                    t.dietary_recommendations AS DietaryRecommendations,
                    t.observation AS Observation,
                    t.is_active AS IsActive,
                    t.continuous_use AS ContinuousUse,
                    t.medication_Id AS MedicationId,
                    t.user_id AS UserId
                FROM treatment t
                WHERE t.is_active = 1
                AND t.user_id = {userId}
                ORDER BY id DESC;
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }
        
        public async Task<List<TreatmentResponseModel>> GetTreatmentDesactives ( int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
              SELECT 
                    t.id AS Id,
                    t.medication_id AS MedicineId,
                    t.name_medication AS NameMedication,
                    t.medication_quantity AS MedicineQuantity,
                    t.start_time AS StartTime,
                    t.treatment_interval_hours AS TreatmentIntervalHours,
                    t.treatment_interval_days AS TreatmentDurationDays,
                    t.dietary_recommendations AS DietaryRecommendations,
                    t.observation AS Observation,
                    t.is_active AS IsActive,
                    t.continuous_use AS ContinuousUse,
                    t.medication_Id AS MedicationId,
                    t.user_id AS UserId
                FROM treatment t
                WHERE t.is_active = 0
                AND t.user_id = {userId}
                ORDER BY id DESC;
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }


        public async Task<List<TreatmentResponseModel>> GetTreatmentInactives ( int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                 SELECT 
                    t.id AS Id,
                    t.medication_id AS MedicineId,
                    t.name_medication AS NameMedication,
                    t.medication_quantity AS MedicineQuantity,
                    t.start_time AS StartTime,
                    t.treatment_interval_hours AS TreatmentIntervalHours,
                    t.treatment_interval_days AS TreatmentDurationDays,
                    t.dietary_recommendations AS DietaryRecommendations,
                    t.observation AS Observation,
                    t.is_active AS IsActive,
                    t.continuous_use AS ContinuousUse,
                    t.medication_Id AS MedicationId,
                    t.user_id AS UserId
                FROM treatment t
                WHERE t.is_active = 0
                AND t.user_id = {userId}
                ORDER BY id DESC
                LIMIT 1
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        public async Task<List<TreatmentResponseModel>> GetTreatmentByInterval ( string startTime, string finishTime, int userId )
        {
            string sql;
            List<TreatmentResponseModel> TreatmentList;
            sql = $@"
                SELECT 
                    t.id AS Id,
                    t.medication_id AS MedicineId,
                    t.name_medication AS NameMedication,
                    t.medication_quantity AS MedicineQuantity,
                    t.start_time AS StartTime,
                    t.treatment_interval_hours AS TreatmentIntervalHours,
                    t.treatment_interval_days AS TreatmentDurationDays,
                    t.dietary_recommendations AS DietaryRecommendations,
                    t.observation AS Observation,
                    t.is_active AS IsActive,
                    t.continuous_use AS ContinuousUse,
                    t.medication_Id AS MedicationId,
                    t.user_id AS UserId
                FROM treatment t
                WHERE t.is_active = 1
                AND t.user_id = {userId};
                AND NOT EXISTS (
                    SELECT 1
                    FROM treatment_management tm
                    WHERE tm.treatment_id = t.id
                    AND tm.correct_time_treatment BETWEEN '{startTime}' AND '{finishTime}'
                    AND tm.medication_intake_date = now()
                    )
                ";

            await Connect();
            await Query(sql);
            TreatmentList = await GetQueryResultList();
            await Disconnect();
            return TreatmentList;
        }

        protected override TreatmentResponseModel Mapper ( DbDataReader reader )
        {
            return _mapper.Map<TreatmentResponseModel>(reader);
        }
        
    }
}
