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
        public async Task<List<TratamentoResponseModel>> GetTreatmentByName(string name)
        {
            List<TratamentoResponseModel> treatmentList = await _treatmentDb.BuscarTratamentoPorNome(name);

            if (treatmentList == null || treatmentList.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return treatmentList;
        }
        public async Task<List<TratamentoResponseModel>> GetIntervalTreatment(string startTime, string finishTime)
        {
            List<TratamentoResponseModel> treatmentList = await _treatmentDb.BuscarTratamentoPorIntervalo(startTime, finishTime);

            if (treatmentList == null || treatmentList.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return treatmentList;
        } 
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoGeral()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoGeral();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoTomado()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoTomado();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoNaoTomado();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico7Dias()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico7Dias();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }

        public async Task<List<HistoricoResponseModel>> BuscarHistorico15Dias()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico15Dias();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico30Dias()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico30Dias();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistorico60Dias()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistorico60Dias();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno()
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoUltimoAno();

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica(string data)
        {
            // Decodifica a string de data
            string dataDecodificada = HttpUtility.UrlDecode(data);

            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoDataEspecifica(dataDecodificada);

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        } 
        public async Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento(string nome)
        {
            List<HistoricoResponseModel> historico;
            historico = await _historicoDb.BuscarHistoricoPorMedicamento(nome);

            if (historico == null || historico.Count == 0)
            {
                throw new RecordNotFoundException();
            }

            return historico;
        }
        public async Task<bool> BuscarStatusDoUltimoGerenciamento ( )
        {
            HistoricoResponseModel historico;
            historico = await _historicoDb.BuscarStatusDoUltimoGerenciamento();

            if (historico == null)
            {
                throw new RecordNotFoundException();
            }
            else if (historico.WasTaken == 0)
                return false;
            return true;
        } 
        public async Task<string> BuscarUltimoGerenciamento ( )
        {
            HistoricoResponseModel historico;
            TratamentoResponseModel tratamentoLista;
            historico = await _historicoDb.BuscarUltimoGerenciamento();
            tratamentoLista = await _treatmentDb.BuscarTratamentoPorId(historico.TreatmentId);
            
            if (historico == null || tratamentoLista == null)
            {
                return null; 
            }

            return tratamentoLista.Name;


        }
    }
}
