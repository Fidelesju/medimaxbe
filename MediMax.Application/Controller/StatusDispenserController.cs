using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class StatusDispenserController : BaseController<StatusDispenserController>
    {
        private readonly ILoggerService _loggerService;
        private readonly IStatusDispenserService _statusDispenserService;

        public StatusDispenserController(
            ILogger<StatusDispenserController> logger,
            IStatusDispenserService statusDispenserService,
            ILoggerService loggerService) : base(logger, loggerService)
        {
            _statusDispenserService = statusDispenserService;
        }

        [HttpPost("CreateOrUpate")]
        public async Task<ActionResult<BaseResponse<int>>> CriandoStatusDispenser(StatusDispenserCreateRequestModel request)
        {
            try
            {
                int id = await _statusDispenserService.CriandoOuAtualizandoStatusDispenser(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Abastecimento feito com sucesso!")
                    .SetData(id);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        [HttpGet("GetStatusDispenser/{treatmentId}")]
        public async Task<ActionResult<BaseResponse<StatusDispenserListaResponseModel>>> BuscandoStatusDispenser(int treatmentId)
        {
            try
            {
                StatusDispenserListaResponseModel statusDispenserResponse;

                statusDispenserResponse = await _statusDispenserService.BuscandoStatusDispenser(treatmentId);
                var response = BaseResponse<StatusDispenserListaResponseModel>.Builder()
                    .SetMessage("Abastecimento feito com sucesso!")
                    .SetData(statusDispenserResponse);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        [HttpPost("CalculatorCountBox")]
        public async Task<ActionResult<BaseResponse<int>>> CalculadoraQuantidadeCaixasTratamento( CalculadoraCaixasRequestModel request )
        {
            try
            {
                int statusDispenserResponse;

                statusDispenserResponse = await _statusDispenserService.CalculadoraQuantidadeCaixasTratamento(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Abastecimento feito com sucesso!")
                    .SetData(statusDispenserResponse);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

    }
}
