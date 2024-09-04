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
    public class TreatmentController : BaseController<TreatmentController>
    {
        private readonly ITreatmentService _treatmentService;
        private readonly ILogger<TreatmentController> _logger;

        public TreatmentController(
            ILogger<TreatmentController> logger,
            ILoggerService loggerService,
            ITreatmentService TreatmentService) : base(logger, loggerService)
        {
            _treatmentService = TreatmentService;
        }


        /// <summary>
        /// Cria um novo Medication e Treatment.
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CreateTreatment ( TreatmentCreateRequestModel request )
        {
            try
            {
                int result = await _treatmentService.CreateTreatment(request);

                if (result == 0)
                    return Ok(BaseResponse<int>
                          .Builder()
                          .SetMessage("Falha ao cadastrar tratamento")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<int>
                         .Builder()
                         .SetMessage("Medications não encontrado")
                         .SetData(result)
                     );
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CriandoMedicationsETreatment: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriandoMedicationsETreatment: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Alterando Medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateMedication ( TreatmentUpdateRequestModel request )
        {
            try
            {
                var result = await _treatmentService.UpdateMedication(request);

                if (result == true)
                    return Ok(BaseResponse<bool>
                          .Builder()
                          .SetMessage("Tratamento alterado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<bool>
                         .Builder()
                         .SetMessage("Falha ao alterar tratamento")
                         .SetData(result)
                     );
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "AlterandoMedicationsETreatment: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AlterandoMedicationsETreatment: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Deletando um tratemento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("deactivate/medication/{medication_id}/treatment/{treatment_id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DeactivateTreatment ( int medication_id, int treatment_id )
        {
            try
            {
                bool result = await _treatmentService.DeactivateTreatment(medication_id, treatment_id);

                var response = new BaseResponse<bool>
                {
                    Message = "Tratamento desativado com sucesso.",
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

        /// <summary>
        /// Deletando um tratemento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reactive/medication/{medication_id}/treatment/{treatment_id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> ReactiveTreatment ( int medication_id, int treatment_id )
        {
            try
            {
                bool result = await _treatmentService.ReactiveTreatment(medication_id, treatment_id);

                var response = new BaseResponse<bool>
                {
                    Message = "Tratamento reativado com sucesso.",
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
       
        [HttpGet("medication/{medicineId}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TreatmentResponseModel>>>> GetTreatmentByMedicationId ( int medicineId, int userId )
        {
            try
            {
                var Treatment = await _treatmentService.GetTreatmentByMedicationId(medicineId, userId);
                var response = BaseResponse<List<TreatmentResponseModel>>
                        .Builder()
                        .SetMessage("Treatments encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return BadRequest($"Nome de Treatment inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return NotFound("Nenhum Treatment encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}");
            }
        }
        
        [HttpGet("dosage-time/{treatmentId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TimeDosageResponseModel>>>> GetDosageTimeByTreatmentId ( int treatmentId )
        {
            try
            {
                var Treatment = await _treatmentService.GetDosageTimeByTreatmentId(treatmentId);
                var response = BaseResponse<List<TimeDosageResponseModel>>
                        .Builder()
                        .SetMessage("Tratamentos encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return BadRequest($"Nome de Treatment inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return NotFound("Nenhum Treatment encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}"); 
            }
        }
         
        [HttpGet("dosage-time/{userId}/{time}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TimeDosageResponseModel>>>> GetDosageTimeByUserIdAndTime ( int userId, string time )
        {
            try
            {
                var Treatment = await _treatmentService.GetDosageTimeByUserIdAndTime(userId, time);
                var response = BaseResponse<List<TimeDosageResponseModel>>
                        .Builder()
                        .SetMessage("Horarios de dosagem encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return BadRequest($"Nome de Treatment inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return NotFound("Nenhum Treatment encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}"); 
            }
        }

        [HttpGet("{treatmentId}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<TreatmentResponseModel>>> GetTreatmentById ( int treatmentId , int userId )
        {
            try
            {
                var Treatment = await _treatmentService.GetTreatmentById(treatmentId, userId);
                var response = BaseResponse<TreatmentResponseModel>
                        .Builder()
                        .SetMessage("Treatments encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return BadRequest($"Nome de Treatment inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return NotFound("Nenhum Treatment encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}");
            }
        }
        
       
        [HttpGet("actives/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TreatmentResponseModel>>>> GetTreatmentActives( int userId )
        {
            try
            {
                var Treatment = await _treatmentService.GetTreatmentActives(userId);
                var response = BaseResponse<List<TreatmentResponseModel>>
                        .Builder()
                        .SetMessage("Treatments encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return BadRequest($"Nome de Treatment inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return NotFound("Nenhum Treatment encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}");
            }
        }

        [HttpGet("deactivate/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TreatmentResponseModel>>>> GetTreatmentDeactivate( int userId )
        {
            try
            {
                var Treatment = await _treatmentService.GetTreatmentDeactivate(userId);
                var response = BaseResponse<List<TreatmentResponseModel>>
                        .Builder()
                        .SetMessage("Treatments encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return BadRequest($"Nome de Treatment inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return NotFound("Nenhum Treatment encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}");
            }
        }

        [HttpGet("interval/startTime/{startTime}/finishTime/{finishTime}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TreatmentResponseModel>>>> GetTreatmentByInterval(string startTime, string finishTime, int userId )
        {
            try
            {
                var Treatment = await _treatmentService.GetTreatmentByInterval(startTime, finishTime, userId);
                var response = BaseResponse<List<TreatmentResponseModel>>
                        .Builder()
                        .SetMessage("Treatments encontrados com sucesso.")
                        .SetData(Treatment);
                return Ok(response);
            }
            catch (InvalidIntervalException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorIntervalo: Controller");
                return BadRequest($"Intervalo de tempo inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorIntervalo: Controller");
                return NotFound("Nenhum Treatment encontrado para o intervalo de tempo especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTreatmentPorIntervalo: Controller");
                return StatusCode(500, $"Erro ao buscar Treatments: {ex.Message}");
            }
        }

        
    }
}
