using Azure;
using MediMax.Application.Controller;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace MediMax.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TreatmentManagementController : BaseController<TreatmentManagementController>
    {
        private readonly ITreatmentManagementService _treatmentManagementService;
        private readonly IRelatoriosService _reportService;
        private readonly ILogger<TreatmentManagementController> _logger;

        public TreatmentManagementController (
            ITreatmentManagementService TreatmentManagementService,
            IRelatoriosService relatoriosService,
            ILogger<TreatmentManagementController> logger,
            ILoggerService loggerService ) : base(logger, loggerService)
        {
            _treatmentManagementService = TreatmentManagementService;
            _reportService = relatoriosService;
        }

        /// <summary>
        /// Criando gerenciamento de Treatment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult> CreateTreatmentManagement ( TreatmentManagementCreateRequestModel request )
        {
            try
            {
                int result = await _treatmentManagementService.CreateTreatmentManagement(request);

                if (result != null)
                    return Ok(BaseResponse<int>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
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
                _logger.LogError(ex, "CriandoTreatmentManagement: Controller");
                return StatusCode(400, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriandoTreatmentManagement: Controller");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        [HttpPost("pdf")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<byte[]>> PdfGenerator ( ReportRequestModel request )
        {
            try
            {
                var pdfBytes = await _reportService.PdfGenerator(request);
                return pdfBytes;
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "GeradorPdf: Controller");
                return StatusCode(400, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GeradorPdf: Controller");
                return StatusCode(500, "Internal server error occurred.");
            }
        }


        /// <summary>
        /// Obtém historico geral.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> GetAllHistoric ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.GetAllHistoric(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoGeral: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoGeral: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico geral.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last-status/user/{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> BuscarStatusDoUltimoGerenciamento ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarStatusDoUltimoGerenciamento(id);

                if (result == true)
                    return Ok(BaseResponse<bool>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<bool>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoGeral: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoGeral: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico geral.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last/user/{id}")]
        public async Task<ActionResult<BaseResponse<string>>> BuscarUltimoGerenciamento ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarUltimoGerenciamento(id);

                if (result != "")
                    return Ok(BaseResponse<string>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<string>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );

            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoGeral: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoGeral: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico tomado.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("was-taken/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistoricoTomado ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistoricoTomado(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoTomado: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoTomado: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico não tomado.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("not-taken/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistoricoNaoTomado ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistoricoNaoTomado(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoNaoTomado: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoNaoTomado: Controller");
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 7 dias.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last-seven-days/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistorico7Dias ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistorico7Dias(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistorico7Dias: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistorico7Dias: Controller");
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 15 dias.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last-fifty-days/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistorico15Dias ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistorico15Dias(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistorico15Dias: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistorico15Dias: Controller");
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 30 dias.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last-thirty-days/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistorico30Dias (int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistorico30Dias(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistorico30Dias: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistorico30Dias: Controller");
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 60 dias.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last-sixty-days/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistorico60Dias ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistorico60Dias(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistorico60Dias: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistorico60Dias: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico ultimo ano.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("last-year/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistoricoUltimoAno ( int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistoricoUltimoAno(id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoUltimoAno: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoUltimoAno: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico data especifica.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("date/{date}/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistoricoDataEspecifica ( string date, int id )
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistoricoDataEspecifica(date, id );

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoDataEspecifica: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoDataEspecifica: Controller");
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico por medicamento.
        /// </summary>
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        [HttpGet("medication/{name}/user/{id}")]
        public async Task<ActionResult<BaseResponse<List<TreatmentManagementResponseModel>>>> BuscarHistoricoPorMedicamento ( string name, int id)
        {
            try
            {
                var result = await _treatmentManagementService.BuscarHistoricoPorMedicamento(name, id);

                if (result != null)
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<TreatmentManagementResponseModel>>
                         .Builder()
                         .SetMessage("Medicamentos não encontrado")
                         .SetData(result)
                     );
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarHistoricoPorMedicamento: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarHistoricoPorMedicamento: Controller");
                return await UntreatedException(ex);
            }
        }
    }
}
