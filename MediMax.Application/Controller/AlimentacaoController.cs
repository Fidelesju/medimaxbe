using MediMax.Application.Controller;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediMax.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlimentacaoController : BaseController<AlimentacaoController>
    {
        private readonly IAlimentacaoService _alimentacaoService;
        private readonly ILogger<AccountController> _logger;
        public AlimentacaoController(
            ILogger<AlimentacaoController> logger,
            ILoggerService loggerService,
            IAlimentacaoService alimentacaoService) : base(logger, loggerService)
        {
            _alimentacaoService = alimentacaoService ?? throw new ArgumentNullException(nameof(alimentacaoService));
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CriarRefeicoes(AlimentacaoCreateRequestModel request)
        {
            try
            {
                int id = await _alimentacaoService.CriarRefeicoes(request);
                if (id == 0)
                {
                    return BadRequest(new BaseResponse<int>
                    {
                        Message = "Falha ao criar uma nova refeição.",
                        Data = 0
                    });
                }

                return Ok(new BaseResponse<int>
                {
                    Message = "Refeição criada com sucesso.",
                    Data = id
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
        public async Task<ActionResult<BaseResponse<bool>>> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request)
        {
            try
            {
                bool success = await _alimentacaoService.AlterandoAlimentacao(request);
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

        [HttpPost("Delete/{id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DeletandoAlimentacao(int id)
        {
            try
            {
                bool success = await _alimentacaoService.DeletandoAlimentacao(id);
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


        [HttpGet("GetMealsByType/TypeMeals/{typeMeals}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<AlimentacaoResponseModel>>>> BuscarRefeicoesPorTipo(string typeMeals)
        {
            try
            {
                List<AlimentacaoResponseModel> alimentacao = await _alimentacaoService.BuscarAlimentacaoPorTipo(typeMeals);
                return Ok(new BaseResponse<List<AlimentacaoResponseModel>>
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

        private async Task<ActionResult> HandleException(Exception exception)
        {
            _logger.LogError(exception, "Erro ao processar a solicitação");
            return StatusCode(500, "Erro interno do servidor");
        }
    }
}
