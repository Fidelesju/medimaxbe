using MediMax.Application.Controllers;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioController>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(
        ILogger<UsuarioController> logger,
            ILoggerService loggerService,
            IUsuarioService usuarioService) : base(logger, loggerService)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
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
                _logger.LogError(ex, "CriarUsuario: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarUsuario: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpGet("UserId/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
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
                _logger.LogError(ex, "BuscarUsuarioPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUsuarioPorId: Controller");
                return await UntreatedException(ex);
            }
        }
    }
}
