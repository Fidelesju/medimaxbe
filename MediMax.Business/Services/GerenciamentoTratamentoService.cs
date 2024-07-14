using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace MediMax.Business.Services
{
    public class TreatmentManagementService : ITreatmentManagementService
    {
        private readonly ITreatmentManagementCreateMapper _TreatmentManagementCreateMapper;
        private readonly ITreatmentManagementRepository _TreatmentManagementRepository;
        private readonly ITreatmentDb _treatmentDb;
        private readonly IHistoricoDb _historicoDb;

        public TreatmentManagementService(
            ITreatmentManagementCreateMapper TreatmentManagementCreateMapper,
            ITreatmentManagementRepository TreatmentManagementRepository,
            ITreatmentDb treatmentDb,
            IHistoricoDb historicoDb)
        {
            _TreatmentManagementCreateMapper = TreatmentManagementCreateMapper;
            _TreatmentManagementRepository = TreatmentManagementRepository;
            _treatmentDb = treatmentDb;
            _historicoDb = historicoDb;
        }

        public async Task<int> CriandoTreatmentManagement(GerencimentoTreatmentCreateRequestModel request)
        {
            TreatmentManagement TreatmentManagement;
            TreatmentManagementCreateValidation validation;
            
            validation = new TreatmentManagementCreateValidation();
            if (!validation.IsValid(request))
            {
                throw new CustomValidationException(validation.GetErrors());
            }

            _TreatmentManagementCreateMapper.SetBaseMapping(request);
            TreatmentManagement = _TreatmentManagementCreateMapper.GetTreatmentManagement();

            try
            {
                _TreatmentManagementRepository.Create(TreatmentManagement);
                return TreatmentManagement.Id;
            }
            catch (DbUpdateException exception)
            {
                throw new CustomValidationException(validation.GetPersistenceErrors(exception));
            }
        }
        public async Task<List<TreatmentResponseModel>> BuscarTreatmentPorMedicamentoId( int medicineId, int userId )
        {
            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _treatmentDb.GetTreatmentByMedicationId(medicineId, userId);
            return treatmentList;
        }
        public async Task<List<TreatmentResponseModel>> GetIntervalTreatment(string startTime, string finishTime, int userId )
        {
            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _treatmentDb.GetTreatmentByInterval(startTime, finishTime, userId);
            return treatmentList;
        } 
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoGeral ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoGeral(userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoTomado ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoTomado(userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoNaoTomado(userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico7Dias ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico7Dias(userId);
            return historico;
        }

        public async Task<List<HistoricoResponseModel>> BuscarHistorico15Dias ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico15Dias(userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico30Dias ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico30Dias(userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico60Dias ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico60Dias(userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno ( int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoUltimoAno( userId);
            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica(string data, int userId )
        {
            // Decodifica a string de data
            string dataDecodificada = HttpUtility.UrlDecode(data);
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoDataEspecifica(dataDecodificada,userId);
            return historico;
        } 
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento(string nome, int userId )
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoPorMedicamento(nome, userId);
            return historico;
        }
        public async Task<bool> BuscarStatusDoUltimoGerenciamento ( int userId )
       {
            HistoricoResponseModel historico;
            historico = await _historicoDb.BuscarStatusDoUltimoGerenciamento(userId);
            if (historico == null)
                return false;
            else if(historico.WasTaken == 0)
                return false;
            return true;
        } 
        public async Task<string> BuscarUltimoGerenciamento ( int userId )
        {
            HistoricoResponseModel historico;
            TreatmentResponseModel TreatmentLista = null;
            int treatmentId = 0;

            historico = await _historicoDb.BuscarUltimoGerenciamento(userId);
            if (historico != null)
                treatmentId = historico.TreatmentId;

            TreatmentLista = await _treatmentDb.GetTreatmentById(treatmentId, userId);
            if (TreatmentLista == null)
                return "";
            else
                return TreatmentLista.Name;
        }
    }
}
