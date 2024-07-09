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
    public class GerenciamentoTratamentoController : BaseController<GerenciamentoTratamentoController>
    {
        private readonly IGerenciamentoTratamentoService _gerenciamentoTratamentoService;
        private readonly IRelatoriosService _relatoriosService;
        private readonly ILogger<GerenciamentoTratamentoController> _logger;

        public GerenciamentoTratamentoController (
            IGerenciamentoTratamentoService gerenciamentoTratamentoService,
            IRelatoriosService relatoriosService,
            ILogger<GerenciamentoTratamentoController> logger,
            ILoggerService loggerService ) : base(logger, loggerService)
        {
            _gerenciamentoTratamentoService = gerenciamentoTratamentoService;
            _relatoriosService = relatoriosService;
        }

        /// <summary>
        /// Criando gerenciamento de tratamento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult> CriandoGerenciamentoTratamento ( GerencimentoTratamentoCreateRequestModel request )
        {
            try
            {
                int result = await _gerenciamentoTratamentoService.CriandoGerenciamentoTratamento(request);

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
                _logger.LogError(ex, "CriandoGerenciamentoTratamento: Controller");
                return StatusCode(400, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriandoGerenciamentoTratamento: Controller");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        [HttpPost("PDF")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<byte[]>> GeradorPdf ( RelatorioRequestModel request )
        {
            try
            {
                var pdfBytes = await _relatoriosService.GeradorPdf(request);
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
        [HttpGet("GetHistoric/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoGeral ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistoricoGeral(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("GetLastStatusTreatment/{userId}")]
        public async Task<ActionResult<BaseResponse<bool>>> BuscarStatusDoUltimoGerenciamento ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarStatusDoUltimoGerenciamento(userId);

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
        [HttpGet("GetLastTreatment/{userId}")]
        public async Task<ActionResult<BaseResponse<string>>> BuscarUltimoGerenciamento ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarUltimoGerenciamento(userId);

                if (result != null)
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
        [HttpGet("GetWasTaken/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoTomado ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistoricoTomado(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("GetNotTaken/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoNaoTomado ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistoricoNaoTomado(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("Get7Days/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico7Dias ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistorico7Dias(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("Get15Days/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico15Dias ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistorico15Dias(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("Get30Days/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico30Dias (int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistorico30Dias(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("Get60Days/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico60Dias ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistorico60Dias(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("GetLastYear/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoUltimoAno ( int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistoricoUltimoAno(userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("GetHistoricByDate/{data}/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoDataEspecifica ( string data, int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistoricoDataEspecifica(data, userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
        [HttpGet("GetHistoricByMedicine/{nome}/{userId}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoPorMedicamento ( string nome, int userId )
        {
            try
            {
                var result = await _gerenciamentoTratamentoService.BuscarHistoricoPorMedicamento(nome, userId);

                if (result != null)
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
                          .Builder()
                          .SetMessage("Medicamentos encontrado com sucesso.")
                          .SetData(result)
                      );
                else
                    return Ok(BaseResponse<List<HistoricoResponseModel>>
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
