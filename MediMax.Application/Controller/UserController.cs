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
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
        ILogger<UserController> logger,
            ILoggerService loggerService,
            IUserService UserService) : base(logger, loggerService)
        {
            _userService = UserService ?? throw new ArgumentNullException(nameof(UserService));
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CriarUser(UserCreateRequestModel request)
        {
            try
            {
                int id = await _userService.CriarUser(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário criado com sucesso.")
                    .SetData(id);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CriarUser: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarUser: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpPost("Update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> AtualizarUser ( UserUpdateRequestModel request)
        {
            try
            {
                int id = await _userService.AtualizarUser(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário alterado com sucesso.")
                    .SetData(id);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "AtualizarUser: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AtualizarUser: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpPost("Desactive")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> DesativarUser ( int userId)
        {
            try
            {
                int user = await _userService.DesativarUser(userId);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário criado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CriarUser: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarUser: Controller");
                return await UntreatedException(ex);
            }
        }
        [HttpPost("Reactive")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> ReativarUser ( int userId)
        {
            try
            {
                int user = await _userService.ReativarUser(userId);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário criado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError(ex, "CriarUser: Controller");
                return ValidationErrorsBadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CriarUser: Controller");
                return await UntreatedException(ex);
            }
        }

        [HttpGet("UserId/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> BuscarUserPorId(int userId)
        {
            try
            {
                var user = await _userService.BuscarUserPorId(userId);
                var response = BaseResponse<UserResponseModel>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
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
                var user = await _userService.AlterarSenha(password,userId);
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
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> BuscarUserPorNome ( string name)
        {
            try
            {
                var user = await _userService.BuscarUserPorNome(name);
                var response = BaseResponse<UserResponseModel>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await UntreatedException(ex);
            }
        }
        
        [HttpGet("Email/{email}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> BuscarUserPorEmail ( string email )
        {
            try
            {
                var user = await _userService.BuscarUserPorEmail(email);
                var response = BaseResponse<UserResponseModel>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await UntreatedException(ex);
            }
        }
        
        [HttpGet("TypeUser/{typeUser}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UserResponseModel>>>> BuscarUserPorTipoDeUser ( int typeUser )
        {
            try
            {
                var user = await _userService.BuscarUserPorTipoDeUser(typeUser);
                var response = BaseResponse<List<UserResponseModel>>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await UntreatedException(ex);
            }
        } 
        
        [HttpGet("TypeUserAndOwner/{typeUser}/{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UserResponseModel>>>> BuscarUserPorProprietarioeTipoDeUser ( int typeUser, int ownerId )
        {
            try
            {
                var user = await _userService.BuscarUserPorProprietarioeTipoDeUser(typeUser, ownerId);
                var response = BaseResponse<List<UserResponseModel>>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await UntreatedException(ex);
            }
        }
        
        [HttpGet("Owner/{ownerId}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UserResponseModel>>>> BuscarUserPorProprietario ( int ownerId )
        {
            try
            {
                var user = await _userService.BuscarUserPorProprietario(ownerId);
                var response = BaseResponse<List<UserResponseModel>>.Builder()
                    .SetMessage("Usuário encontrado com sucesso.")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await UntreatedException(ex);
            }
        }
        [HttpPost("SendEmail/{email}")]
        public async Task<ActionResult<BaseResponse<EmailCodigoResponseModel>>> EnviarEmailCodigo ( string email )
        {
            try
            {
                EmailCodigoResponseModel user = await _userService.EnviarEmailCodigo(email);
                var response = BaseResponse<EmailCodigoResponseModel>.Builder()
                    .SetMessage("Codigo enviado com sucesso!")
                    .SetData(user);
                return Ok(response);
            }
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await NotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuscarUserPorId: Controller");
                return await UntreatedException(ex);
            }
        }
    }
}
