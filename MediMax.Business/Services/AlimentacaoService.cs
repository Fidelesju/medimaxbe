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
        private readonly IDetalheAlimentacaoCreateMapper _foodDetailCreateMapper;
        private readonly IAlimentacaoRepository _alimentacaoRepository;
        private readonly IDetalheAlimentacaoRepository _detalheAlimentacaoRepository;
        private readonly IAlimentacaoDb _alimentacaoDb;
        public AlimentacaoService (
            IAlimentacaoCreateMapper foodCreateMapper,
            IAlimentacaoRepository alimentacaoRepository,
            IDetalheAlimentacaoRepository detalheAlimentacaoRepository,
            IDetalheAlimentacaoCreateMapper foodDetailCreateMapper,
            IAlimentacaoDb alimentacaoDb) 
        {
            _foodCreateMapper = foodCreateMapper;
            _alimentacaoRepository = alimentacaoRepository;
            _alimentacaoDb = alimentacaoDb;
            _detalheAlimentacaoRepository = detalheAlimentacaoRepository;
            _foodDetailCreateMapper = foodDetailCreateMapper;
        }

        public async Task<int> CriarRefeicoes(AlimentacaoCreateRequestModel request)
        {
            Alimentacao food = null;
            DetalheAlimentacao detailFood;
            DetalheAlimentacaoCreateRequestModel detailRequest;
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
                foreach (var detalheAlimentacao in request.detalhe_alimentacao)
                {
                    detailRequest = new DetalheAlimentacaoCreateRequestModel();
                    detailRequest.alimento = detalheAlimentacao.alimento;
                    detailRequest.unidade_medida = detalheAlimentacao.unidade_medida;
                    detailRequest.quantidade = detalheAlimentacao.quantidade;

                    detailFood = _foodDetailCreateMapper.GetFoodDetail(detailRequest);
                    detailFood.id = 0;
                    _detalheAlimentacaoRepository.Create(detailFood);
                    request.detalhe_alimentacao_id = detailFood.id;
                    food = _foodCreateMapper.GetFood();
                    food.id = 0;
                    _alimentacaoRepository.Create(food);
                }
                return food.detalhe_alimentacao_id;
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
                foreach (var detalheAlimentacao in request.detalhe_alimento)
                {
                    success = await _alimentacaoDb.AlterandoDetalheAlimentacao(detalheAlimentacao.quantidade,detalheAlimentacao.alimento, detalheAlimentacao.unidade_medida, detalheAlimentacao.id);
                }
                return success;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
        } 
        
        public async Task<bool> DeletandoAlimentacao(int id, int userId)
        {
            bool success;
            try
            {
                success = await _alimentacaoDb.DeletandoAlimentacao(id, userId);
                return success;
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException();
            }
        }

        public async Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals, int userId )
        {
            List<AlimentacaoResponseModel> alimentacao;
            alimentacao = await _alimentacaoDb.BuscarAlimentacaoPorTipo(typeMeals, userId);
            if (alimentacao == null)
            {
                throw new RecordNotFoundException();
            }
            return alimentacao;
        }
        
        public async Task<AlimentacaoResponseModel> BuscarRefeicoesPorHorario ( int userId )
        {
            AlimentacaoResponseModel alimentacao;
            alimentacao = await _alimentacaoDb.BuscarRefeicoesPorHorario( userId);
            if (alimentacao == null)
            {
                return alimentacao;
            }
            return alimentacao;
        }
    }
}
