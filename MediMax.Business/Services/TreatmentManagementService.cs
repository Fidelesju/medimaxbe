using AutoMapper;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using System.Web;

namespace MediMax.Business.Services
{
    public class TreatmentManagementService : ITreatmentManagementService
    {
        private readonly ITreatmentManagementRepository _treatmentManagementRepository;
        private readonly ITreatmentDb _treatmentDb;
        private readonly ITreatmentManagementDbDb _historicoDb;
        private IMapper _mapper;

        public TreatmentManagementService(
            ITreatmentManagementRepository TreatmentManagementRepository,
            ITreatmentDb treatmentDb,
            ITreatmentManagementDbDb historicoDb,
            IMapper mapper)
        {
            _treatmentManagementRepository = TreatmentManagementRepository;
            _treatmentDb = treatmentDb;
            _historicoDb = historicoDb;
            _mapper = mapper;
        }

        public async Task<int> CreateTreatmentManagement(TreatmentManagementCreateRequestModel request)
        {
            var result = new BaseResponse<int>();
            TreatmentManagementCreateValidation validation = new TreatmentManagementCreateValidation();

            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            var treatmentManagement = _mapper.Map<TreatmentManagement>(request);
            _treatmentManagementRepository.Create(treatmentManagement);
            result.Data = treatmentManagement.Id;
            result.IsSuccess = true;

            if (treatmentManagement.Id != 0)
            {
                result.IsSuccess = true;
                result.Data = treatmentManagement.Id;
            }
            else
            {
                result.IsSuccess = false;
                result.Data = 0;
                return result.Data;
            }
            return result.Data;
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
        public async Task<List<TreatmentManagementResponseModel>> GetAllHistoric ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.GetAllHistoric(userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoTomado ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoTomado(userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoNaoTomado ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoNaoTomado(userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico7Dias ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico7Dias(userId);
            return historico;
        }

        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico15Dias ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico15Dias(userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico30Dias ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico30Dias(userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistorico60Dias ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico60Dias(userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoUltimoAno ( int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoUltimoAno( userId);
            return historico;
        }
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoDataEspecifica(string data, int userId )
        {
            // Decodifica a string de data
            string dataDecodificada = HttpUtility.UrlDecode(data);
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoDataEspecifica(dataDecodificada,userId);
            return historico;
        } 
        public async Task<List<TreatmentManagementResponseModel>> BuscarHistoricoPorMedicamento(string nome, int userId )
        {
            List<TreatmentManagementResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoPorMedicamento(nome, userId);
            return historico;
        }
        public async Task<bool> BuscarStatusDoUltimoGerenciamento ( int userId )
       {
            TreatmentManagementResponseModel historico;
            historico = await _historicoDb.BuscarStatusDoUltimoGerenciamento(userId);
            if (historico == null)
                return false;
            else if(historico.Was_Taken == 0)
                return false;
            return true;
        } 
        public async Task<string> BuscarUltimoGerenciamento ( int userId )
        {
            TreatmentManagementResponseModel historico;
            TreatmentResponseModel TreatmentLista = null;
            int treatmentId = 0;

            historico = await _historicoDb.BuscarUltimoGerenciamento(userId);
            if (historico != null)
                treatmentId = historico.Treatment_Id;

            TreatmentLista = await _treatmentDb.GetTreatmentById(treatmentId, userId);
            if (TreatmentLista == null)
                return "";
            else
                return TreatmentLista.Name;
        }
    }
}
