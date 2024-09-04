using MediMax.Application.Controller;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NutritionController : BaseController<NutritionController>
    {
        private readonly INutritionService _nutritionService;
        private readonly ILogger<AccountController> _logger;

        public NutritionController(
            ILogger<NutritionController> logger,
            ILoggerService loggerService,
            INutritionService nutritionService) : base(logger, loggerService)
        {
            _nutritionService = nutritionService ?? throw new ArgumentNullException(nameof(nutritionService));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CreateNutrition(NutritionCreateRequestModel request )
        {
            try
            {
                var response = await _nutritionService.CreateNutrition(request);
                if (response == null)
                {
                    return Ok(new BaseResponse<int>
                    {
                        Message = "Falha ao criar uma nova refeição.",
                        Data = response.Data
                    });
                }

                return Ok(new BaseResponse<int>
                {
                    Message = "Refeição criada com sucesso.",
                    Data = response.Data
                });
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CriarRefeicoes: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarRefeicoes: Controller");
                return await HandleException(ex);
            }
        }
        
        [HttpPost("update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateNutrition ( NutritionUpdateRequestModel request )
        {
            try
            {
                var success = await _nutritionService.UpdateNutrition(request);
                if (!success.Data)
                {
                    return BadRequest(new BaseResponse<bool>
                    {
                        Message = "Falha ao alterar dados de refeição.",
                        Data = success.Data
                    });
                }

                return Ok(new BaseResponse<bool>
                {
                    Message = "Refeição alterada com sucesso.",
                    Data = success.IsSuccess
                });
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "AlterandoAlimentacao: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AlterandoAlimentacao: Controller");
                return await HandleException(ex);
            }
        }

        [HttpPost("desactive")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DesactiveNutrition( NutritionDesativeRequestModel request )
        {
            try
            {
                var success = await _nutritionService.DesactiveNutrition(request);
                if (!success.Data)
                {
                    return BadRequest(new BaseResponse<bool>
                    {
                        Message = "Falha ao deletar refeição.",
                        Data = success.Data
                    });
                }

                return Ok(new BaseResponse<bool>
                {
                    Message = "Refeição deletada com sucesso.",
                    Data = success.Data
                });
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "DeletandoAlimentacao: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeletandoAlimentacao: Controller");
                return await HandleException(ex);
            }
        }

        [HttpPost("reactive")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> ReactiveNutrition( NutritionReactiveRequestModel request )
        {
            try
            {
                var success = await _nutritionService.ReactiveNutrition(request);
                if (!success.Data)
                {
                    return BadRequest(new BaseResponse<bool>
                    {
                        Message = "Falha ao deletar refeição.",
                        Data = success.Data
                    });
                }

                return Ok(new BaseResponse<bool>
                {
                    Message = "Refeição deletada com sucesso.",
                    Data = success.Data
                });
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "DeletandoAlimentacao: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeletandoAlimentacao: Controller");
                return await HandleException(ex);
            }
        }


        [HttpGet("by-type/{typeNutrition}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<NutritionGetResponseModel>>>> GetNutritionByNutritionType(string typeNutrition, int userId )
        {
            try
            {
                List<NutritionGetResponseModel> alimentacao = await _nutritionService.GetNutritionByType(typeNutrition, userId);
                return Ok(new BaseResponse<List<NutritionGetResponseModel>>
                {
                    Message = "Alimentos encontrados com sucesso.",
                    Data = alimentacao
                });
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarRefeicoesPorTipo: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarRefeicoesPorTipo: Controller");
                return await HandleException(ex);
            }
        }
        
        [HttpGet("user/{id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<NutritionGetResponseModel>>>> GetNutritionByUserId( int id )
        {
            try
            {
               List<NutritionGetResponseModel> alimentacao = await _nutritionService.GetNutritionByUserId(id);

                if(alimentacao != null)
                return Ok(new BaseResponse<List<NutritionGetResponseModel>>
                {
                    Message = "Alimentos encontrados com sucesso.",
                    Data = alimentacao
                });
                else
                    return Ok(new BaseResponse<List<NutritionGetResponseModel>>
                    {
                        Message = "Alimentos não encontrados.",
                        Data = alimentacao
                    });
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarRefeicoesPorTipo: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarRefeicoesPorTipo: Controller");
                return await HandleException(ex);
            }
        }
             
        [HttpGet("by-details/{nutritionId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<NutritionDetailResponseModel>>>> GetNutritionDetailsByUserIAndNutritionId( int nutritionId )
        {
            try
            {
                List<NutritionDetailResponseModel> alimentacao = await _nutritionService.GetNutritionDetailsByUserIAndNutritionId(nutritionId);
                if(alimentacao != null)
                return Ok(new BaseResponse<List<NutritionDetailResponseModel>>
                {
                    Message = "Alimentos encontrados com sucesso.",
                    Data = alimentacao
                });
                else
                    return Ok(new BaseResponse<List<NutritionDetailResponseModel>>
                    {
                        Message = "Alimentos não encontrados.",
                        Data = alimentacao
                    });
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarRefeicoesPorTipo: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarRefeicoesPorTipo: Controller");
                return await HandleException(ex);
            }
        }

        private async Task<ActionResult> HandleException(Exception exception)
        {
            _logger.LogError(exception, "Erro ao processar a solicitação");
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}
