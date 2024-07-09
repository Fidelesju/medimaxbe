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
    public class GerenciamentoTratamentoService : IGerenciamentoTratamentoService
    {
        private readonly IGerenciamentoTratamentoCreateMapper _gerenciamentoTratamentoCreateMapper;
        private readonly IGerenciamentoTratamentoRepository _gerenciamentoTratamentoRepository;
        private readonly ITratamentoDb _treatmentDb;
        private readonly IHistoricoDb _historicoDb;

        public GerenciamentoTratamentoService(
            IGerenciamentoTratamentoCreateMapper gerenciamentoTratamentoCreateMapper,
            IGerenciamentoTratamentoRepository gerenciamentoTratamentoRepository,
            ITratamentoDb treatmentDb,
            IHistoricoDb historicoDb)
        {
            _gerenciamentoTratamentoCreateMapper = gerenciamentoTratamentoCreateMapper;
            _gerenciamentoTratamentoRepository = gerenciamentoTratamentoRepository;
            _treatmentDb = treatmentDb;
            _historicoDb = historicoDb;
        }

        public async Task<int> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request)
        {
            GerenciamentoTratamento gerenciamentoTratamento;
            GerenciamentoTratamentoCreateValidation validation;
            
            validation = new GerenciamentoTratamentoCreateValidation();
            if (!validation.IsValid(request))
            {
                throw new CustomValidationException(validation.GetErrors());
            }

            _gerenciamentoTratamentoCreateMapper.SetBaseMapping(request);
            gerenciamentoTratamento = _gerenciamentoTratamentoCreateMapper.GetGerenciamentoTratamento();

            try
            {
                _gerenciamentoTratamentoRepository.Create(gerenciamentoTratamento);
                return gerenciamentoTratamento.id;
            }
            catch (DbUpdateException exception)
            {
                throw new CustomValidationException(validation.GetPersistenceErrors(exception));
            }
        }
        public async Task<List<TratamentoResponseModel>> GetTreatmentByName(string name, int userId )
        {
            List<TratamentoResponseModel> treatmentList;
            treatmentList = await _treatmentDb.BuscarTratamentoPorNome(name, userId);
            return treatmentList;
        }
        public async Task<List<TratamentoResponseModel>> GetIntervalTreatment(string startTime, string finishTime, int userId )
        {
            List<TratamentoResponseModel> treatmentList;
            treatmentList = await _treatmentDb.BuscarTratamentoPorIntervalo(startTime, finishTime, userId);
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
            if (historico.WasTaken == 0)
                return false;
            return true;
        } 
        public async Task<string> BuscarUltimoGerenciamento ( int userId )
        {
            HistoricoResponseModel historico;
            TratamentoResponseModel tratamentoLista;
            historico = await _historicoDb.BuscarUltimoGerenciamento(userId);
            tratamentoLista = await _treatmentDb.BuscarTratamentoPorId(historico.TreatmentId, userId);
            if( tratamentoLista == null )
                return "";
            else
                return tratamentoLista.Name;
        }
    }
}
