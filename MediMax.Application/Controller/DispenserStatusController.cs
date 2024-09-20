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
    public class DispenserStatusController : BaseController<DispenserStatusController>
    {
        private readonly ILoggerService _loggerService;
        private readonly IDispenserStatusService _DispenserStatusService;

        public DispenserStatusController(
            ILogger<DispenserStatusController> logger,
            IDispenserStatusService DispenserStatusService,
            ILoggerService loggerService) : base(logger, loggerService)
        {
            _DispenserStatusService = DispenserStatusService;
        }

        [HttpPost("CreateOrUpate")]
        public async Task<ActionResult<BaseResponse<int>>> CriandoDispenserStatus(int treatmentId, int userId, int medicationId)
        {
            try
            {
                int id = await _DispenserStatusService.CriandoOuAtualizandoDispenserStatus(treatmentId,userId,medicationId);
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

        [HttpGet("GetDispenserStatus/{treatmentId}/{userId}")]
        public async Task<ActionResult<DispenserStatusListaResponseModel>> BuscandoDispenserStatus(int treatmentId, int userId )
        {
            try
            {
                DispenserStatusListaResponseModel DispenserStatusResponse;

                DispenserStatusResponse = await _DispenserStatusService.BuscandoDispenserStatus(treatmentId, userId);
                var response = BaseResponse<DispenserStatusListaResponseModel>.Builder()
                    .SetMessage("Abastecimento feito com sucesso!")
                    .SetData(DispenserStatusResponse);
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
        public async Task<ActionResult<BaseResponse<int>>> CalculadoraQuantidadeCaixasTreatment( CalculadoraCaixasRequestModel request )
        {
            try
            {
                int DispenserStatusResponse;

                DispenserStatusResponse = await _DispenserStatusService.CalculadoraQuantidadeCaixasTreatment(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Abastecimento feito com sucesso!")
                    .SetData(DispenserStatusResponse);
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
