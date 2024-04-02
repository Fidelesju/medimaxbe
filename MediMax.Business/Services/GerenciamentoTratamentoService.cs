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

namespace MediMax.Business.Services
{
    public class GerenciamentoTratamentoService : IGerenciamentoTratamentoService
    {
        private readonly IGerenciamentoTratamentoCreateMapper _gerenciamentoTratamentoCreateMapper;
        private readonly IGerenciamentoTratamentoRepository _gerenciamentoTratamentoRepository;
        private readonly ITratamentoDb _treatmentDb;

        public GerenciamentoTratamentoService(
            IGerenciamentoTratamentoCreateMapper gerenciamentoTratamentoCreateMapper,
            IGerenciamentoTratamentoRepository gerenciamentoTratamentoRepository,
            ITratamentoDb treatmentDb)
        {
            _gerenciamentoTratamentoCreateMapper = gerenciamentoTratamentoCreateMapper;
            _gerenciamentoTratamentoRepository = gerenciamentoTratamentoRepository;
            _treatmentDb = treatmentDb;
        }

        public async Task<int> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request)
        {
            GerenciamentoTratamento gerenciamentoTratamento;
            GerenciamentoTratamentoCreateValidation validation = new GerenciamentoTratamentoCreateValidation();

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
    }
}
