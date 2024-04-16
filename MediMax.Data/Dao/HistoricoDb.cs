using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using System.Data.Common;

namespace MediMax.Data.Dao
{
    public class HistoricoDb : Db<HistoricoResponseModel>, IHistoricoDb
    {
        public HistoricoDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<HistoricoResponseModel>> BuscarHistoricoGeral()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id as Id,
	                gt.tratamento_id as TratamentoId,
                    gt.data_ingestao_medicamento as DataIngestaoMedicamento,
                    gt.horario_correto_tratamento as HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado
                FROM gerenciamento_tratamento gt
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoTomado()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id as Id,
	                gt.tratamento_id as TratamentoId,
                    gt.data_ingestao_medicamento as DataIngestaoMedicamento,
                    gt.horario_correto_tratamento as HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.foi_tomado = 1
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id as Id,
	                gt.tratamento_id as TratamentoId,
                    gt.data_ingestao_medicamento as DataIngestaoMedicamento,
                    gt.horario_correto_tratamento as HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento as HorarioIngestaoMedicamento,
                    gt.foi_tomado as FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.foi_tomado = 0
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico7Dias()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.data_ingestao_medicamento >= CURDATE() - INTERVAL 7 DAY
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico15Dias()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.data_ingestao_medicamento >= CURDATE() - INTERVAL 15 DAY
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico30Dias()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.data_ingestao_medicamento >= CURDATE() - INTERVAL 30 DAY
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico60Dias()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.data_ingestao_medicamento >= CURDATE() - INTERVAL 60 DAY
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno()
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.data_ingestao_medicamento >= CURDATE() - INTERVAL 1 YEAR
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }

        public async Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica(string data)
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                WHERE gt.data_ingestao_medicamento = '{data}'
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }
        
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento(string nome)
        {
            string sql;
            List<HistoricoResponseModel> historicoResponseModel;
            sql = $@"
                SELECT 
                    gt.id AS Id,
                    gt.tratamento_id AS TratamentoId,
                    gt.data_ingestao_medicamento AS DataIngestaoMedicamento,
                    gt.horario_correto_tratamento AS HorarioCorretoTratamento,
                    gt.horario_ingestao_medicamento AS HorarioIngestaoMedicamento,
                    gt.foi_tomado AS FoiTomado
                FROM gerenciamento_tratamento gt
                INNER JOIN tratamento t ON t.id = gt.tratamento_id
                INNER JOIN medicamentos m ON m.id = t.remedio_id
                WHERE m.nome  = '{nome}'
                ";

            await Connect();
            await Query(sql);
            historicoResponseModel = await GetQueryResultList();
            await Disconnect();
            return historicoResponseModel;
        }

        protected override HistoricoResponseModel Mapper(DbDataReader reader)
        {
            HistoricoResponseModel historico;
            historico = new HistoricoResponseModel();
            historico.Id = Convert.ToInt32(reader["Id"]);
            historico.TreatmentId = Convert.ToInt32(reader["TratamentoId"]);
            historico.CorrectTreatmentSchedule = Convert.ToString(reader["DataIngestaoMedicamento"]);
            historico.MedicationIntakeSchedule = Convert.ToString(reader["HorarioCorretoTratamento"]);
            historico.DateMedicationIntake = Convert.ToString(reader["HorarioIngestaoMedicamento"]);
            historico.WasTaken = Convert.ToInt32(reader["FoiTomado"]);
            return historico;
        }
    }
}
