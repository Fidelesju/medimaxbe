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

namespace MediMax.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GerenciamentoTratamentoController : BaseController<GerenciamentoTratamentoController>
    {
        private readonly IGerenciamentoTratamentoService _gerenciamentoTratamentoService;
        private readonly IRelatoriosService _relatoriosService;
        private readonly ILogger<GerenciamentoTratamentoController> _logger;

        public GerenciamentoTratamentoController(
            IGerenciamentoTratamentoService gerenciamentoTratamentoService,
            IRelatoriosService relatoriosService,
            ILogger<GerenciamentoTratamentoController> logger,
            ILoggerService loggerService) : base(logger, loggerService)
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
        public async Task<ActionResult> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request)
        {
            try
            {
                int id = await _gerenciamentoTratamentoService.CriandoGerenciamentoTratamento(request);
                if (id == 0)
                {
                    return BadRequest(new BaseResponse<int>
                    {
                        Message = "Falha ao cadastrar gerenciamento de tratamento."
                    });
                }

                return Ok(new BaseResponse<int>
                {
                    Message = "Gerenciamento de tratamento cadastrado com sucesso.",
                    Data = id
                });
                
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CriandoGerenciamentoTratamento: Controller");
                return StatusCode(400,ex);
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
        [HttpGet("GetHistoric")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoGeral()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistoricoGeral();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };
                return Ok(response);
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
        [HttpGet("GetLastStatusTreatment")]
        public async Task<ActionResult<BaseResponse<bool>>> BuscarStatusDoUltimoGerenciamento()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarStatusDoUltimoGerenciamento();

                var response = new BaseResponse<bool>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };
                return Ok(response);
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
        [HttpGet("GetLastTreatment")]
        public async Task<ActionResult<BaseResponse<string>>> BuscarUltimoGerenciamento()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarUltimoGerenciamento();

                if(medicine == null)
                {
                    var response = new BaseResponse<string>
                    {
                        Message = "Historico não encontrado.",
                        Data = medicine
                    };
                    return BadRequest(response);
                }
                else
                {
                    var response = new BaseResponse<string>
                    {
                        Message = "Historico encontrado com sucesso.",
                        Data = medicine
                    };
                    return Ok(medicine);
                }

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
        [HttpGet("GetWasTaken")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoTomado()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistoricoTomado();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("GetNotTaken")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoNaoTomado()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistoricoNaoTomado();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("Get7Days")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico7Dias()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistorico7Dias();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("Get15Days")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico15Dias()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistorico15Dias();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("Get30Days")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico30Dias()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistorico30Dias();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("Get60Days")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistorico60Dias()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistorico60Dias();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("GetLastYear")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoUltimoAno()
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistoricoUltimoAno();

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("GetHistoricByDate/{data}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoDataEspecifica(string data)
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistoricoDataEspecifica(data);

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
        [HttpGet("GetHistoricByMedicine/{nome}")]
        public async Task<ActionResult<BaseResponse<List<HistoricoResponseModel>>>> BuscarHistoricoPorMedicamento(string nome)
        {
            try
            {
                var medicine = await _gerenciamentoTratamentoService.BuscarHistoricoPorMedicamento(nome);

                var response = new BaseResponse<List<HistoricoResponseModel>>
                {
                    Message = "Historico encontrado com sucesso.",
                    Data = medicine
                };

                return Ok(response);
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
