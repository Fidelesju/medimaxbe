using MediMax.Application.Controllers;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Enums;
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

        [HttpPost("Update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> AtualizarUsuario ( UsuarioUpdateRequestModel request)
        {
            try
            {
                int id = await _usuarioService.AtualizarUsuario(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário alterado com sucesso.")
                    .SetData(id);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "AtualizarUsuario: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AtualizarUsuario: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpPost("Desactive")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> DesativarUsuario ( int userId)
        {
            try
            {
                int user = await _usuarioService.DesativarUsuario(userId);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário criado com sucesso.")
                    .SetData(user);
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
        [HttpPost("Reactive")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> ReativarUsuario ( int userId)
        {
            try
            {
                int user = await _usuarioService.ReativarUsuario(userId);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário criado com sucesso.")
                    .SetData(user);
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
       
        [HttpPost("UpdatePassword/{password}/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> AlterarSenha(int userId, string password)
        {
            try
            {
                var user = await _usuarioService.AlterarSenha(password,userId);
                var response = BaseResponse<bool>.Builder()
                    .SetMessage("Senha alterada com sucesso!")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "AlterarSenha: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AlterarSenha: Controller");
                return await UntreatedException(ex);
            }
        }
        
        [HttpGet("Name/{name}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UsuarioResponseModel>>> BuscarUsuarioPorNome ( string name)
        {
            try
            {
                var user = await _usuarioService.BuscarUsuarioPorNome(name);
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
        
        [HttpGet("Email/{email}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UsuarioResponseModel>>> BuscarUsuarioPorEmail ( string email )
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
                _logger.LogError(ex, "BuscarUsuarioPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUsuarioPorId: Controller");
                return await UntreatedException(ex);
            }
        }
        
        [HttpGet("TypeUser/{typeUser}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UsuarioResponseModel>>>> BuscarUsuarioPorTipoDeUsuario ( int typeUser )
        {
            try
            {
                var user = await _usuarioService.BuscarUsuarioPorTipoDeUsuario(typeUser);
                var response = BaseResponse<List<UsuarioResponseModel>>.Builder()
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
        
        [HttpGet("TypeUserAndOwner/{typeUser}/{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UsuarioResponseModel>>>> BuscarUsuarioPorProprietarioeTipoDeUsuario ( int typeUser, int ownerId )
        {
            try
            {
                var user = await _usuarioService.BuscarUsuarioPorProprietarioeTipoDeUsuario(typeUser, ownerId);
                var response = BaseResponse<List<UsuarioResponseModel>>.Builder()
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
        
        [HttpGet("Owner/{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UsuarioResponseModel>>>> BuscarUsuarioPorProprietario ( int ownerId )
        {
            try
            {
                var user = await _usuarioService.BuscarUsuarioPorProprietario(ownerId);
                var response = BaseResponse<List<UsuarioResponseModel>>.Builder()
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
        [HttpPost("SendEmail/{email}")]
        public async Task<ActionResult<BaseResponse<EmailCodigoResponseModel>>> EnviarEmailCodigo ( string email )
        {
            try
            {
                EmailCodigoResponseModel user = await _usuarioService.EnviarEmailCodigo(email);
                var response = BaseResponse<EmailCodigoResponseModel>.Builder()
                    .SetMessage("Codigo enviado com sucesso!")
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
