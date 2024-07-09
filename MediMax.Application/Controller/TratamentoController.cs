using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services;
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

        [HttpGet("Name/{name}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TratamentoResponseModel>>>> BuscarTratamentoPorNome(string name, int userId )
        {
            try
            {
                var tratamento = await _tratamentoService.BuscarTratamentoPorNome(name, userId);
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

        [HttpGet("GetTreatmentById/{treatmentId}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<TratamentoResponseModel>>> BuscarTratamentoPorId ( int treatmentId , int userId )
        {
            try
            {
                var tratamento = await _tratamentoService.BuscarTratamentoPorId(treatmentId, userId);
                var response = BaseResponse<TratamentoResponseModel>
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
        
       
        [HttpGet("GetAllTreatmentActives/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TratamentoResponseModel>>>> BuscarTodosTratamentoAtivos( int userId )
        {
            try
            {
                var tratamento = await _tratamentoService.BuscarTodosTratamentoAtivos(userId);
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

        [HttpGet("StartTime/{startTime}/FinishTime/{finishTime}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<TratamentoResponseModel>>>> BuscarTratamentoPorIntervalo(string startTime, string finishTime, int userId )
        {
            try
            {
                var tratamento = await _tratamentoService.BuscarTratamentoPorIntervalo(startTime, finishTime, userId);
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

        /// <summary>
        /// Deletando um tratemento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Delete/{id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> DeletandoTratamento(int id)
        {
            try
            {
                bool result = await _tratamentoService.DeletandoTratamento(id);

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
