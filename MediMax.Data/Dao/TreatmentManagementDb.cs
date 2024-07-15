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
                    tm.medication_id AS MedicationId
                FROM treatment_management tm
                WHERE tm.treatment_user_id = {userId}
                ORDER BY tm.id DESC
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
                    tm.medication_id AS MedicationId
                FROM treatment_management tm
                WHERE tm.treatment_user_id = {userId}
                ORDER BY tm.id DESC
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
                    gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                ORDER BY gt.id DESC
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
                    gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND gt.foi_tomado = 1
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
                    gt.id as Id,
	                gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado,
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND gt.foi_tomado = 0
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
                    gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND STR_TO_DATE(gt.data_ingestao_medication, '%d/%m/%Y') >= now() - INTERVAL 7 DAY
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
                                   gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND STR_TO_DATE(gt.data_ingestao_medication, '%d/%m/%Y') >= now() - INTERVAL 15 DAY
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
                                  gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND STR_TO_DATE(gt.data_ingestao_medication, '%d/%m/%Y') >= now() - INTERVAL 30 DAY
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
                                    gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND STR_TO_DATE(gt.data_ingestao_medication, '%d/%m/%Y') >= now() - INTERVAL 60 DAY
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
                                   gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND STR_TO_DATE(gt.data_ingestao_medication, '%d/%m/%Y') >= now() - INTERVAL 1 YEAR
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
                    gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND YEAR(STR_TO_DATE(gt.data_ingestao_medication, '%d/%m/%Y')) = '{year}'
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
                    gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON gt.Treatment_id = t.id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND gt.data_ingestao_medication = '{data}'
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
                                   gt.id as Id,
                    gt.Treatment_id as TreatmentId,
                    gt.data_ingestao_medication as DataIngestaoMedicamento,
                    gt.horario_correto_Treatment as HorarioCorretoTreatment,
                    gt.horario_ingestao_medication as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado, 
                    t.nome_medication as NomeMedicamento,
                    m.UserId as UserId
                FROM gerenciamento_Treatment gt
                INNER JOIN Treatment t ON t.id = gt.Treatment_id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.UserId = {userId}
                AND m.nome  = '%{nome}%'
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
