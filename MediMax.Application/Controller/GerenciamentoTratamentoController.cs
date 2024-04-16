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
        private readonly ILogger<GerenciamentoTratamentoController> _logger;

        public GerenciamentoTratamentoController(
            IGerenciamentoTratamentoService gerenciamentoTratamentoService,
            ILogger<GerenciamentoTratamentoController> logger,
            ILoggerService loggerService) : base(logger, loggerService)
        {
            _gerenciamentoTratamentoService = gerenciamentoTratamentoService;
        }

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
                _logger.LogError(ex, "Validation error occurred while creating treatment management.");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred while creating treatment management.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        /// <summary>
        /// Obtém historico geral.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }
        
        /// <summary>
        /// Obtém historico tomado.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }
        
        /// <summary>
        /// Obtém historico não tomado.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 7 dias.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 15 dias.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 30 dias.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }


        /// <summary>
        /// Obtém historico ultimos 60 dias.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico ultimo ano.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico data especifica.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        /// <summary>
        /// Obtém historico por medicamento.
        /// </summary>
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
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }
    }
}
