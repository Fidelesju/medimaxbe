using MediMax.Application.Controllers;
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
        private readonly ILogger<MedicamentoController> _logger;

        public MedicamentoController(
            ILogger<MedicamentoController> logger,
            IMedicamentoService medicamentoService,
            ILoggerService loggerService) : base(logger, loggerService)
        {
            _medicamentoService = medicamentoService;
        }

        /// <summary>
        /// Cria um novo medicamento e tratamento.
        /// </summary>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
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
                _logger.LogError(ex, "CriandoMedicamentosETratamento: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriandoMedicamentosETratamento: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém todos os medicamentos.
        /// </summary>
        [HttpGet("GetAllMedicine")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
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
                _logger.LogError(ex, "BuscarTodosMedicamentos: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTodosMedicamentos: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém lista de medicamentos por nome.
        /// </summary>
        [HttpGet("GetMedicineByName/{name}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicamentoResponseModel>>>> BuscarMedicamentosPorNome(string name)
        {
            try
            {
                var medicine = await _medicamentoService.BuscarMedicamentosPorNome(name);

                var response = new BaseResponse<List<MedicamentoResponseModel>>
                {
                    Message = "Medicamentos encontrados com sucesso.",
                    Data = medicine
                };

                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarMedicamentosPorNome: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarMedicamentosPorNome: Controller");
                return await UntreatedException(ex);
            }
        }
        
        // <summary>
        /// Obtém lista de medicamentos por id de tratamento.
        /// </summary>
        [HttpGet("GetMedicineByTreatmentId/{treatmentId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<MedicamentoResponseModel>>> BuscarMedicamentosPorTratamento(int treatmentId)
        {
            try
            {
                var medicine = await _medicamentoService.BuscarMedicamentosPorTratamento(treatmentId);

                var response = new BaseResponse<MedicamentoResponseModel>
                {
                    Message = "Medicamentos encontrados com sucesso.",
                    Data = medicine
                };

                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarMedicamentosPorNome: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarMedicamentosPorNome: Controller");
                return await UntreatedException(ex);
            }
        }
        
        /// <summary>
        /// Obtém medicamentos por data de vencimento.
        /// </summary>
        [HttpGet("GetMedicineByExpirationDate")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicamentoResponseModel>>>> BuscarMedicamentosPorDataVencimento()
        {
            try
            {
                var medicine = await _medicamentoService.BuscarMedicamentosPorDataVencimento();

                var response = new BaseResponse<List<MedicamentoResponseModel>>
                {
                    Message = "Medicamentos encontrados com sucesso.",
                    Data = medicine
                };

                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarMedicamentosPorDataVencimento: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarMedicamentosPorDataVencimento: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Alterando medicamento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> AlterandoMedicamentosETratamento(MedicamentoETratamentoUpdateRequestModel request)
        {
            try
            {
                bool result = await _medicamentoService.AlterandoMedicamentosETratamento(request);

                var response = new BaseResponse<bool>
                {
                    Message = "Medicamento alterado com sucesso.",
                    Data = result
                };

                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "AlterandoMedicamentosETratamento: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AlterandoMedicamentosETratamento: Controller");
                return await UntreatedException(ex);
            }
        }
   
        /// <summary>
        /// Deletando um medicamento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Delete/{medicineId}/{treatmentId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DeletandoMedicamento(int medicineId, int treatmentId)
        {
            try
            {
                bool result = await _medicamentoService.DeletandoMedicamento(medicineId, treatmentId);

                var response = new BaseResponse<bool>
                {
                    Message = "Medicamento deletado com sucesso.",
                    Data = result
                };

                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "DeletandoMedicamento: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeletandoMedicamento: Controller");
                return await UntreatedException(ex);
            }
        }
  
    }
}
