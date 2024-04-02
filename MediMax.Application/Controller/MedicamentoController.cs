using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controller
{
    [Route("[controller]"), ApiController]
    public class MedicamentoController : BaseController<MedicamentoController>
    {
        private readonly IMedicamentoService _medicamentoService;

        public MedicamentoController(
            ILogger<MedicamentoController> logger,
            ILoggerService loggerService,
            IMedicamentoService medicamentoService) : base(logger, loggerService)
        {
            _medicamentoService = medicamentoService;
        }

        /// <summary>
        /// Cria um novo medicamento e tratamento.
        /// </summary>
        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> CriandoMedicamentosETratamento(MedicamentoETratamentoCreateRequestModel request)
        {
            try
            {
                int id = await _medicamentoService.CriandoMedicamentosETratamento(request);

                var response = new BaseResponse<int>
                {
                    Message = "Medicamento cadastrado com sucesso.",
                    Data = id
                };

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

        /// <summary>
        /// Obtém todos os medicamentos.
        /// </summary>
        [HttpGet("GetAllMedicine")]
        public async Task<ActionResult<BaseResponse<List<MedicamentoResponseModel>>>> BuscarTodosMedicamentos()
        {
            try
            {
                var medicine = await _medicamentoService.BuscarTodosMedicamentos();

                var response = new BaseResponse<List<MedicamentoResponseModel>>
                {
                    Message = "Medicamentos encontrados com sucesso.",
                    Data = medicine
                };

                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }
    }
}
