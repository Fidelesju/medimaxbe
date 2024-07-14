using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using System.Data.Common;

namespace MediMax.Data.Dao
{
    public class HistoricoDb : Db<HistoricoResponseModel>, IHistoricoDb
    {
        public HistoricoDb ( IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext ) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<HistoricoResponseModel>> BuscarHistoricoGeral ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<HistoricoResponseModel> BuscarStatusDoUltimoGerenciamento ( int userId )
        {
            string sql;
            HistoricoResponseModel historicoResponseModel;
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
        public async Task<HistoricoResponseModel> BuscarUltimoGerenciamento ( int userId )
        {
            string sql;
            HistoricoResponseModel historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoTomado ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistorico7Dias ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistorico15Dias ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistorico30Dias ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistorico60Dias ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno ( int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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

        public async Task<List<HistoricoResponseModel>> BuscarHistoricoAnoEspecifico ( string year, int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica ( string data, int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento ( string nome, int userId )
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
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

        protected override HistoricoResponseModel Mapper ( DbDataReader reader )
        {
            HistoricoResponseModel historico;
            historico = new HistoricoResponseModel();
            historico.Id = Convert.ToInt32(reader["Id"]);
            historico.TreatmentId = Convert.ToInt32(reader["TreatmentId"]);
            historico.CorrectTreatmentSchedule = Convert.ToString(reader["HorarioCorretoTreatment"]);
            historico.MedicationIntakeSchedule = Convert.ToString(reader["DataIngestaoMedicamento"]);
            historico.DateMedicationIntake = Convert.ToString(reader["HorarioIngestaoMedicamento"]);
            historico.WasTaken = Convert.ToInt32(reader["FoiTomado"]);
            historico.MedicineName = Convert.ToString(reader["NomeMedicamento"]);
            historico.UserId = Convert.ToInt32(reader["UserId"]);
            return historico;
        }
    }
}
