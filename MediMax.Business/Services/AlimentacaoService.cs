using Microsoft.EntityFrameworkCore;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using MediMax.Data.Models;
using MediMax.Business.Mappers;
using MediMax.Data.Dao;
using MediMax.Data.Repositories;

namespace MediMax.Business.Services
{
    public class AlimentacaoService  : IAlimentacaoService 
    {
        private readonly IAlimentacaoCreateMapper _foodCreateMapper;
        private readonly IAlimentacaoRepository _alimentacaoRepository;
        private readonly IAlimentacaoDb _alimentacaoDb;
        public AlimentacaoService (
            IAlimentacaoCreateMapper foodCreateMapper,
            IAlimentacaoRepository alimentacaoRepository,
            IAlimentacaoDb alimentacaoDb) 
        {
            _foodCreateMapper = foodCreateMapper;
            _alimentacaoRepository = alimentacaoRepository;
            _alimentacaoDb = alimentacaoDb;
        }

        public async Task<int> CriarRefeicoes(AlimentacaoCreateRequestModel request)
        {
            Alimentacao food;
            AlimentacaoCreateValidation validation;
            Dictionary<string, string> errors;

            _foodCreateMapper.SetBaseMapping(request);
            validation = new AlimentacaoCreateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }
            try
            {
                food = _foodCreateMapper.GetFood();
                _alimentacaoRepository.Create(food);
                return food.id;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                if (errors.Count == 0)
                {
                    throw;
                }
                throw new CustomValidationException(errors);
            }
        }

        public async Task<bool> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request)
        {
            AlimentacaoUpdateValidation validation;
            Dictionary<string, string> errors;
            Alimentacao alimentacao;
            bool success;

            validation = new AlimentacaoUpdateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                success = await _alimentacaoDb.AlterandoAlimentacao(request);
                return success;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
        } 
        
        public async Task<bool> DeletandoAlimentacao(int id)
        {
            bool success;
            try
            {
                success = await _alimentacaoDb.DeletandoAlimentacao(id);
                return success;
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException();
            }
        }

        public async Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals)
        {
            List<AlimentacaoResponseModel> alimentacao;
            alimentacao = await _alimentacaoDb.BuscarAlimentacaoPorTipo(typeMeals);
            if (alimentacao == null)
            {
                throw new RecordNotFoundException();
            }
            return alimentacao;
        }
        
        public async Task<AlimentacaoResponseModel> BuscarRefeicoesPorHorario ( )
        {
            AlimentacaoResponseModel alimentacao;
            alimentacao = await _alimentacaoDb.BuscarRefeicoesPorHorario();
            if (alimentacao == null)
            {
                throw new RecordNotFoundException();
            }
            return alimentacao;
        }
    }
}
