using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioController>
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(
        ILogger<UsuarioController> logger,
            ILoggerService loggerService,
            IUsuarioService usuarioService) : base(logger, loggerService)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> CriarUsuario(UsuarioCreateRequestModel request)
        {
            try
            {
                int id = await _usuarioService.CriarUsuario(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário criado com sucesso.")
                    .SetData(id);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                return await UntreatedException(ex);
            }
        }

        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult<BaseResponse<UsuarioResponseModel>>> BuscarUsuarioPorId(int userId)
        {
            try
            {
                var user = await _usuarioService.BuscarUsuarioPorId(userId);
                var response = BaseResponse<UsuarioResponseModel>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
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

        [HttpGet("Login")]
        public async Task<ActionResult<BaseResponse<UsuarioResponseModel>>> BuscarUsarioPorEmail([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var user = await _usuarioService.BuscarUsuarioPorEmail(email);
                var response = BaseResponse<UsuarioResponseModel>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
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
