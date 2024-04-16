using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TratamentoController : BaseController<TratamentoController>
    {
        private readonly ITratamentoService _tratamentoService;
        private readonly ILogger<TratamentoController> _logger;

        public TratamentoController(
            ILogger<TratamentoController> logger,
            ILoggerService loggerService,
            ITratamentoService tratamentoService) : base(logger, loggerService)
        {
            _tratamentoService = tratamentoService;
        }

        [HttpGet("Name/{name}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TratamentoResponseModel>>>> BuscarTratamentoPorNome(string name)
        {
            try
            {
                var tratamento = await _tratamentoService.BuscarTratamentoPorNome(name);
                var response = BaseResponse<List<TratamentoResponseModel>>
                        .Builder()
                        .SetMessage("Tratamentos encontrados com sucesso.")
                        .SetData(tratamento);
                return Ok(response);
            }
            catch (InvalidNameException ex)
            {
                _logger.LogError(ex, "BuscarTratamentoPorNome: Controller");
                return BadRequest($"Nome de tratamento inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTratamentoPorNome: Controller");
                return NotFound("Nenhum tratamento encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTratamentoPorNome: Controller");
                return StatusCode(500, $"Erro ao buscar tratamentos: {ex.Message}");
            }
        }

        [HttpGet("StartTime/{startTime}/FinishTime/{finishTime}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TratamentoResponseModel>>>> BuscarTratamentoPorIntervalo(string startTime, string finishTime)
        {
            try
            {
                var tratamento = await _tratamentoService.BuscarTratamentoPorIntervalo(startTime, finishTime);
                var response = BaseResponse<List<TratamentoResponseModel>>
                        .Builder()
                        .SetMessage("Tratamentos encontrados com sucesso.")
                        .SetData(tratamento);
                return Ok(response);
            }
            catch (InvalidIntervalException ex)
            {
                _logger.LogError(ex, "BuscarTratamentoPorIntervalo: Controller");
                return BadRequest($"Intervalo de tempo inválido: {ex.Message}");
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarTratamentoPorIntervalo: Controller");
                return NotFound("Nenhum tratamento encontrado para o intervalo de tempo especificado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarTratamentoPorIntervalo: Controller");
                return StatusCode(500, $"Erro ao buscar tratamentos: {ex.Message}");
            }
        }
    }
}
