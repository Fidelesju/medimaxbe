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
    public class NutritionService  : INutritionService 
    {
        private readonly INutritionCreateMapper _nutritionCreateMapper;
        private readonly IDetalheAlimentacaoCreateMapper _nutritionDetailCreateMapper;
        private readonly INutritionRepository _nutritionRepository;
        private readonly IDetalheAlimentacaoRepository _detalheAlimentacaoRepository;
        private readonly INutritionDb _nutritionDb;
        public NutritionService (
            INutritionCreateMapper nutritionCreateMapper,
            INutritionRepository nutritionRepository,
            IDetalheAlimentacaoRepository detalheAlimentacaoRepository,
            IDetalheAlimentacaoCreateMapper nutritionDetailCreateMapper,
            INutritionDb alimentacaoDb) 
        {
            _nutritionCreateMapper = nutritionCreateMapper;
            _nutritionRepository = nutritionRepository;
            _nutritionDb = alimentacaoDb;
            _detalheAlimentacaoRepository = detalheAlimentacaoRepository;
            _nutritionDetailCreateMapper = nutritionDetailCreateMapper;
        }

        public async Task<int> CreateNutrition(NutritionCreateRequestModel request)
        {
            Nutrition nutrition = null;
            NutritionDetail detailFood;
            DetalheAlimentacaoCreateRequestModel detailRequest;
            NutritionCreateValidation validation;
            Dictionary<string, string> errors;

            _nutritionCreateMapper.SetBaseMapping(request);
            validation = new NutritionCreateValidation();
            if (!validation.IsValid(request))
            {
                return 0;
            }
            try
            {
                foreach (var detalheAlimentacao in request.detalhe_alimentacao)
                {
                    detailRequest = new DetalheAlimentacaoCreateRequestModel();
                    detailRequest.alimento = detalheAlimentacao.alimento;
                    detailRequest.unidade_medida = detalheAlimentacao.unidade_medida;
                    detailRequest.quantidade = detalheAlimentacao.quantidade;

                    detailFood = _nutritionDetailCreateMapper.GetFoodDetail(detailRequest);
                    detailFood.Id = 0;
                    _detalheAlimentacaoRepository.Create(detailFood);
                    request.detalhe_alimentacao_id = detailFood.Id;
                    nutrition = _nutritionCreateMapper.GetFood();
                    nutrition.Id = 0;
                    _nutritionRepository.Create(nutrition);
                }
                return nutrition.NutritionDetailId;
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
            Nutrition alimentacao;
            bool success;

            validation = new AlimentacaoUpdateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                success = await _nutritionDb.AlterandoAlimentacao(request);
                foreach (var detalheAlimentacao in request.detalhe_alimento)
                {
                    success = await _nutritionDb.AlterandoDetalheAlimentacao(detalheAlimentacao.quantidade,detalheAlimentacao.alimento, detalheAlimentacao.unidade_medida, detalheAlimentacao.id);
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
                success = await _nutritionDb.DeletandoAlimentacao(id, userId);
                return success;
            }
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException();
            }
        }

        public async Task<List<NutritionResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals, int userId )
        {
            List<NutritionResponseModel> alimentacao;
            alimentacao = await _nutritionDb.BuscarAlimentacaoPorTipo(typeMeals, userId);
            if (alimentacao == null)
            {
                throw new RecordNotFoundException();
            }
            return alimentacao;
        }
        
        public async Task<NutritionResponseModel> BuscarRefeicoesPorHorario ( int userId )
        {
            NutritionResponseModel alimentacao;
            alimentacao = await _nutritionDb.BuscarRefeicoesPorHorario( userId);
            if (alimentacao == null)
            {
                return alimentacao;
            }
            return alimentacao;
        }
    }
}
