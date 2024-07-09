using MediMax.Application.Controllers;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

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
                int result = await _medicamentoService.CriandoMedicamentosETratamento(request);

                if (result != null)
                    return Ok(BaseResponse<int>
                          .Builder()
                          .SetMessage("Medicamentos deletado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<int>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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
        [HttpGet("GetAllMedicine/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicamentoResponseModel>>>> BuscarTodosMedicamentos( int userId )
        {
            try
            {
                var result = await _medicamentoService.BuscarTodosMedicamentos(userId);

                if (result != null)
                    return Ok(BaseResponse<List<MedicamentoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos deletado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<MedicamentoResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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
        [HttpGet("GetMedicineByName/{name}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicamentoResponseModel>>>> BuscarMedicamentosPorNome(string name, int userId )
        {
            try
            {
                var result = await _medicamentoService.BuscarMedicamentosPorNome(name, userId);

                if ( result != null )
                    return Ok(BaseResponse<List<MedicamentoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<MedicamentoResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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
        [HttpGet("GetMedicineByTreatmentId/{treatmentId}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<MedicamentoResponseModel>>> BuscarMedicamentosPorTratamento(int treatmentId, int userId )
        {
            try
            {
                var result = await _medicamentoService.BuscarMedicamentosPorTratamento(treatmentId, userId);

                if (result != null)
                    return Ok(BaseResponse<MedicamentoResponseModel>
                          .Builder()
                          .SetMessage("Medicamentos encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<MedicamentoResponseModel>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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
        [HttpGet("GetMedicineByExpirationDate/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicamentoResponseModel>>>> BuscarMedicamentosPorDataVencimento( int userId )
        {
            try
            {
                var result = await _medicamentoService.BuscarMedicamentosPorDataVencimento(userId);

                if (result != null)
                    return Ok(BaseResponse<List<MedicamentoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<MedicamentoResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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

                if (result == true)
                    return Ok(BaseResponse<bool>
                          .Builder()
                          .SetMessage("Medicamentos alterado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<bool>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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
        [HttpPost("Delete/{medicineId}/{treatmentId}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DeletandoMedicamento(int medicineId, int treatmentId, int userId )
        {
            try
            {
                bool result = await _medicamentoService.DeletandoMedicamento(medicineId, treatmentId, userId);

                if (result == true)
                    return Ok(BaseResponse<bool>
                          .Builder()
                          .SetMessage("Medicamentos deletado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<bool>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
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
