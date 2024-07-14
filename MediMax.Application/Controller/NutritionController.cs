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

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CreateNutrition(NutritionCreateRequestModel request)
        {
            try
            {
                int response = await _nutritionService.CreateNutrition(request);
                if (response == 0 || response == null)
                {
                    return Ok(new BaseResponse<int>
                    {
                        Message = "Falha ao criar uma nova refeição.",
                        Data = 0
                    });
                }

                return Ok(new BaseResponse<int>
                {
                    Message = "Refeição criada com sucesso.",
                    Data = response
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
        
        [HttpPost("Update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> AlterandoAlimentacao( AlimentacaoUpdateRequestModel request)
        {
            try
            {
                bool success = await _nutritionService.AlterandoAlimentacao(request);
                if (!success)
                {
                    return BadRequest(new BaseResponse<bool>
                    {
                        Message = "Falha ao alterar dados de refeição.",
                        Data = success
                    });
                }

                return Ok(new BaseResponse<bool>
                {
                    Message = "Refeição alterada com sucesso.",
                    Data = success
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

        [HttpPost("Delete/{nutritionId}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DeletandoAlimentacao(int nutritionId, int userId )
        {
            try
            {
                bool success = await _nutritionService.DeletandoAlimentacao(nutritionId, userId);
                if (!success)
                {
                    return BadRequest(new BaseResponse<bool>
                    {
                        Message = "Falha ao deletar refeição.",
                        Data = success
                    });
                }

                return Ok(new BaseResponse<bool>
                {
                    Message = "Refeição deletada com sucesso.",
                    Data = success
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


        [HttpGet("GetMealsByType/{typeMeals}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<NutritionResponseModel>>>> BuscarRefeicoesPorTipo(string typeMeals, int userId )
        {
            try
            {
                List<NutritionResponseModel> alimentacao = await _nutritionService.BuscarAlimentacaoPorTipo(typeMeals, userId);
                return Ok(new BaseResponse<List<NutritionResponseModel>>
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
        
        [HttpGet("GetMealsByTime/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<NutritionResponseModel>>> BuscarRefeicoesPorHorario( int userId )
        {
            try
            {
                NutritionResponseModel alimentacao = await _nutritionService.BuscarRefeicoesPorHorario(userId);
                if(alimentacao != null)
                return Ok(new BaseResponse<NutritionResponseModel>
                {
                    Message = "Alimentos encontrados com sucesso.",
                    Data = alimentacao
                });
                else
                    return Ok(new BaseResponse<NutritionResponseModel>
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
