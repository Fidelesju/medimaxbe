using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediMax.Application.Controller
{
    [Route("[controller]"), ApiController]
    public class TreatmentController : BaseController<TreatmentController>
    {
        private readonly ITreatmentService _treatmentService;
        public TreatmentController(
            ILogger<TreatmentController> logger,
            ILoggerService loggerService,
            ITreatmentService treatmentService) : base(logger, loggerService)
        {
            _treatmentService = treatmentService;
        }


        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("Name/{name}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentResponseModel>>>> GetTreatmentByName(string name)
        {
            List<TreatmentResponseModel> Treatment;
            BaseResponse<List<TreatmentResponseModel>> response;
            try
            {
                Treatment = await _treatmentService.GetTreatmentByName(name);
                response = BaseResponse<List<TreatmentResponseModel>>
                        .Builder()
                        .SetMessage("Treatments encontrados com sucesso.")
                        .SetData(Treatment)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

    }
}
