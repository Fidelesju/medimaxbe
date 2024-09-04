using AutoMapper;
using FluentValidation;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MediMax.Business.Services
{
    public class NutritionService  : INutritionService 
    {
        private readonly INutritionRepository _nutritionRepository;
        private readonly INutritionDetailRepository _nutritionDetailRepository;
        private readonly INutritionDb _nutritionDb;
        private readonly INutritionDetailDb _nutritionDetailDb;
        private readonly IMapper _mapper;

        public NutritionService (
            INutritionRepository nutritionRepository,
            INutritionDetailRepository nutritionDetailRepository,
            INutritionDetailDb nutritionDetailDb,
            IMapper mapper,
            INutritionDb alimentacaoDb) 
        {
            _nutritionRepository = nutritionRepository;
            _nutritionDb = alimentacaoDb;
            _mapper = mapper;
            _nutritionDetailRepository = nutritionDetailRepository;
            _nutritionDetailDb = nutritionDetailDb;
        }

        public async Task<BaseResponse<int>> CreateNutrition(NutritionCreateRequestModel request)
        {
            var result = new BaseResponse<int>();
            NutritionDetailCreateRequestModel detailRequest;
            Dictionary<string, string> errors;
            NutritionCreateValidation validation;
            validation = new NutritionCreateValidation();

            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return result;
            }

            try
            {
                var nutrition = _mapper.Map<Nutrition>(request);
                nutrition.Is_Active = 1;
                _nutritionRepository.Create(nutrition);

                foreach (var nutritionDetail in request.Nutrition_Detail)
                {
                    detailRequest = new NutritionDetailCreateRequestModel();
                    detailRequest.Nutrition = nutritionDetail.Nutrition;
                    detailRequest.Unit_Measurement = nutritionDetail.Unit_Measurement;
                    detailRequest.Quantity = nutritionDetail.Quantity;
                    detailRequest.Nutrition_Id = nutrition.Id;

                    var nutritionDetailModel = _mapper.Map<NutritionDetail>(detailRequest);
                    _nutritionDetailRepository.Create(nutritionDetailModel);
                    result.Data = nutrition.Id;
                    result.IsSuccess = true;
                    result.SetMessage("Alimentação criado com sucesso!");
                }

                return result;
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

        /// <summary>
        /// Alterando medicamentos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<BaseResponse<bool>> UpdateNutrition ( NutritionUpdateRequestModel request )
        {
            var result = new BaseResponse<bool>();
            NutritionDetailUpdateRequestModel detailRequest;
            Dictionary<string, string> errors;
            NutritionUpdateValidation validation;
            validation = new NutritionUpdateValidation();

            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return result;
            }

            try
            {
                var nutrition = _mapper.Map<NutritionUpdateResponseModel>(request);
                await _nutritionRepository.Update(nutrition);

                foreach (var nutritionDetail in request.Nutrition_Detail)
                {
                    detailRequest = new NutritionDetailUpdateRequestModel();
                    detailRequest.Nutrition = nutritionDetail.Nutrition;
                    detailRequest.Unit_Measurement = nutritionDetail.Unit_Measurement;
                    detailRequest.Quantity = nutritionDetail.Quantity;
                    detailRequest.Nutrition_Id = nutrition.Id;
                    detailRequest.Id = nutritionDetail.Id;

                    var nutritionDetailModel = _mapper.Map<NutritionDetailResponseModel>(detailRequest);
                    _nutritionDetailRepository.Update(nutritionDetailModel);
                    result.Data = true;
                    result.IsSuccess = true;
                }

              
                return result;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
        }

        public async Task<BaseResponse<bool>> DesactiveNutrition ( NutritionDesativeRequestModel request )
        {
            var result = new BaseResponse<bool>();

            try
            {
                await _nutritionRepository.Desactive(request);

                foreach (var nutritionDetail in request.Nutrition_Detail)
                {
                    var detailRequest = new NutritionDetailDeleteRequestModel
                    {
                        Nutrition_Id = nutritionDetail.Nutrition_Id,
                        Id = nutritionDetail.Id
                    };

                    await _nutritionDetailRepository.Delete(detailRequest);
                }

                result.Data = true;
                result.IsSuccess = true;
            }
            catch (DbUpdateException)
            {
                result.Data = false;
                result.IsSuccess = false;
                throw new RecordNotFoundException();
            }

            return result;
        }

        public async Task<BaseResponse<bool>> ReactiveNutrition ( NutritionReactiveRequestModel request )
        {
            var result = new BaseResponse<bool>();
            NutritionDetailCreateRequestModel detailRequest;

            try
            {
                await _nutritionRepository.Reactive(request);

                foreach (var nutritionDetail in request.Nutrition_Detail)
                {
                    detailRequest = new NutritionDetailCreateRequestModel();
                    detailRequest.Nutrition = nutritionDetail.Nutrition;
                    detailRequest.Unit_Measurement = nutritionDetail.Unit_Measurement;
                    detailRequest.Quantity = nutritionDetail.Quantity;
                    detailRequest.Nutrition_Id = request.Id;

                    var nutritionDetailModel = _mapper.Map<NutritionDetail>(detailRequest);
                    _nutritionDetailRepository.Create(nutritionDetailModel);
                    result.Data = true;
                    result.IsSuccess = true;
                    result.SetMessage("Alimentação criado com sucesso!");
                }

                result.Data = true;
                result.IsSuccess = true;
            }
            catch (DbUpdateException)
            {
                result.Data = false;
                result.IsSuccess = false;
                throw new RecordNotFoundException();
            }

            return result;
        }

        public async Task<List<NutritionGetResponseModel>> GetNutritionByType(string nutritionType, int userId )
        {
            List<NutritionGetResponseModel> nutrition;
            nutrition = await _nutritionDb.GetNutritionByNutritionType(nutritionType, userId);
            if (nutrition == null)
            {
                throw new RecordNotFoundException();
            }
            return nutrition;
        }
        
        public async Task<List<NutritionGetResponseModel>> GetNutritionByUserId ( int userId )
        {
            List<NutritionGetResponseModel> nutrition;
            nutrition = await _nutritionDb.GetNutritionByUserId( userId);
            if (nutrition == null)
            {
                return nutrition;
            }
            return nutrition;
        }

        public async Task<List<NutritionDetailResponseModel>> GetNutritionDetailsByUserIAndNutritionId (int nutritionId )
        {
            List<NutritionDetailResponseModel> alimentacao;
            alimentacao = await _nutritionDetailDb.GetNutritionDetailsByUserIAndNutritionId( nutritionId);
            if (alimentacao == null)
            {
                return alimentacao;
            }
            return alimentacao;
        }
    }
}
