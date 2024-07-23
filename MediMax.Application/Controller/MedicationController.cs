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
    public class MedicationController : BaseController<MedicationController>
    {
        private readonly IMedicationService _medicationService;
        private readonly ILogger<MedicationController> _logger;

        public MedicationController(
            ILogger<MedicationController> logger,
            IMedicationService medicationService,
            ILoggerService loggerService) : base(logger, loggerService)
        {
            _medicationService = medicationService;
        }

        /// <summary>
        /// Cria um novo Medication e Treatment.
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CreateMedication(MedicationCreateRequestModel request)
        {
            try
            {
                var result = await _medicationService.CreateMedication(request);

                if (result.IsSuccess)
                    return Ok(BaseResponse<int>
                          .Builder()
                          .SetMessage("Medicamento cadastrado com sucesso.")
                          .SetMessage(result.Message)
                          .SetData(result.Data)
                      );
                else
                    return Ok(BaseResponse<int>
                         .Builder()
                         .SetMessage(result.Message)
                         .SetData(result.Data)
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
        public async Task<ActionResult<BaseResponse<bool>>> UpdateMedication ( MedicationUpdateRequestModel request )
        {
            try
            {
                bool result = await _medicationService.UpdateMedication(request);

                if (result == true)
                    return Ok(BaseResponse<bool>
                          .Builder()
                          .SetMessage("Medications alterado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<bool>
                         .Builder()
                         .SetMessage("Medications não encontrado")
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
        /// Deletando um Medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("desactive/medication/{medicineId}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DesactiveMedication ( int medicineId, int userId )
          {
                try
                {
                    bool result = await _medicationService.DesactiveMedication(medicineId, userId);

                    if (result == true)
                        return Ok(BaseResponse<bool>
                              .Builder()
                              .SetMessage("Medications deletado com sucesso.")
                              .SetData(result)
                          );
                    else
                        return Ok(BaseResponse<bool>
                             .Builder()
                             .SetMessage("Medications não encontrado")
                             .SetData(result)
                         );
                }
                catch (CustomValidationException ex)
                {
                    _logger.LogError(ex, "DeletandoMedication: Controller");
                    return ValidationErrorsBadRequest(ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "DeletandoMedication: Controller");
                    return await UntreatedException(ex);
                }
            }

        /// <summary>
        /// Deletando um Medication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reactive/medication/{medicineId}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> ReactiveMedication ( int medicineId, int userId )
          {
                try
                {
                    bool result = await _medicationService.ReactiveMedication(medicineId, userId);

                    if (result == true)
                        return Ok(BaseResponse<bool>
                              .Builder()
                              .SetMessage("Medications deletado com sucesso.")
                              .SetData(result)
                          );
                    else
                        return Ok(BaseResponse<bool>
                             .Builder()
                             .SetMessage("Medications não encontrado")
                             .SetData(result)
                         );
                }
                catch (CustomValidationException ex)
                {
                    _logger.LogError(ex, "DeletandoMedication: Controller");
                    return ValidationErrorsBadRequest(ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "DeletandoMedication: Controller");
                    return await UntreatedException(ex);
                }
            }

        /// <summary>
        /// Obtém todos os Medications.
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicationResponseModel>>>> GetAllMedicine( int userId )
        {
            try
            {
                var result = await _medicationService.GetAllMedicine(userId);

                if (result != null)
                    return Ok(BaseResponse<List<MedicationResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<MedicationResponseModel>>
                         .Builder()
                         .SetMessage("Medications não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTodosMedications: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTodosMedications: Controller");
                return await UntreatedException(ex);
            }
        }

      
        /// <summary>
        /// Obtém todos os Medications.
        /// </summary>
        [HttpGet("{medicationId}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<MedicationResponseModel>>> GetMedicationById( int medicationId, int userId )
        {
            try
            {
                var result = await _medicationService.GetMedicationById(medicationId, userId);

                if (result != null)
                    return Ok(BaseResponse<MedicationResponseModel>
                          .Builder()
                          .SetMessage("Medicamentos encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<MedicationResponseModel>
                         .Builder()
                         .SetMessage("Medications não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTodosMedications: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTodosMedications: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém lista de Medications por nome.
        /// </summary>
        [HttpGet("by-name/{name}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicationResponseModel>>>> GetMedicationByName(string name, int userId )
        {
            try
            {
                var result = await _medicationService.GetMedicationByName(name, userId);

                if ( result != null )
                    return Ok(BaseResponse<List<MedicationResponseModel>>
                          .Builder()
                          .SetMessage("Medications encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<MedicationResponseModel>>
                         .Builder()
                         .SetMessage("Medications não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarMedicationsPorNome: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarMedicationsPorNome: Controller");
                return await UntreatedException(ex);
            }
        }
        
        // <summary>
        /// Obtém lista de Medications por id de Treatment.
        /// </summary>
        [HttpGet("by-treatment/{treatmentId}/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<MedicationResponseModel>>> GetMedicationByTreatmentId(int treatmentId, int userId )
        {
            try
            {
                var result = await _medicationService.GetMedicationByTreatmentId(treatmentId, userId);

                if (result != null)
                    return Ok(BaseResponse<MedicationResponseModel>
                          .Builder()
                          .SetMessage("Medications encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<MedicationResponseModel>
                         .Builder()
                         .SetMessage("Medications não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarMedicationsPorNome: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarMedicationsPorNome: Controller");
                return await UntreatedException(ex);
            }
        }
        
        /// <summary>
        /// Obtém Medications por data de vencimento.
        /// </summary>
        [HttpGet("by-expiration-date/user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<MedicationResponseModel>>>> GetMedicationByExpirationDate( int userId )
        {
            try
            {
                var result = await _medicationService.GetMedicationByExpirationDate(userId);

                if (result != null)
                    return Ok(BaseResponse<List<MedicationResponseModel>>
                          .Builder()
                          .SetMessage("Medications encontrados com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<MedicationResponseModel>>
                         .Builder()
                         .SetMessage("Medications não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarMedicationsPorDataVencimento: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarMedicationsPorDataVencimento: Controller");
                return await UntreatedException(ex);
            }
        }

    }
}
