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
    public class GerenciamentoTratamentoController : ControllerBase
    {
        private readonly IGerenciamentoTratamentoService _gerenciamentoTratamentoService;
        private readonly ILogger<GerenciamentoTratamentoController> _logger;

        public GerenciamentoTratamentoController(
            IGerenciamentoTratamentoService gerenciamentoTratamentoService,
            ILogger<GerenciamentoTratamentoController> logger)
        {
            _gerenciamentoTratamentoService = gerenciamentoTratamentoService ?? throw new ArgumentNullException(nameof(gerenciamentoTratamentoService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Create")]

        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]

        public async Task<IActionResult> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request)
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

        private IActionResult ValidationErrorsBadRequest(CustomValidationException ex)
        {
            var errors = new BaseResponse<int>
            {
                Message = "Validation errors occurred.",
                Data = 0
            };
            return BadRequest(errors);
        }
    }
}
