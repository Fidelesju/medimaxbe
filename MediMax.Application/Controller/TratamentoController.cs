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

        public TratamentoController(
            ILogger<TratamentoController> logger,
            ILoggerService loggerService,
            ITratamentoService tratamentoService) : base(logger, loggerService)
        {
            _tratamentoService = tratamentoService;
        }

        [HttpGet("Name/{name}")]
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
                return BadRequest($"Nome de tratamento inválido: {ex.Message}");
            }
            catch (RecordNotFoundException)
            {
                return NotFound("Nenhum tratamento encontrado com o nome especificado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar tratamentos: {ex.Message}");
            }
        }

        [HttpGet("StartTime/{startTime}/FinishTime/{finishTime}")]
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
                return BadRequest($"Intervalo de tempo inválido: {ex.Message}");
            }
            catch (RecordNotFoundException)
            {
                return NotFound("Nenhum tratamento encontrado para o intervalo de tempo especificado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar tratamentos: {ex.Message}");
            }
        }
    }
}
