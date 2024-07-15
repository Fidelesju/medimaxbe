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

        [HttpPost("create")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> CreateUser( UserCreateRequestModel request)
        {
            try
            {
                var result = await _userService.CreateUser(request);

                if(result == 0)
                {
                    return Ok(BaseResponse<int>
                         .Builder()
                         .SetMessage("Tratamento alterado com sucesso.")
                         .SetData(result)
                     );
                }

                return Ok(BaseResponse<int>
                         .Builder()
                         .SetMessage("Tratamento alterado com sucesso.")
                         .SetData(result)
                     );

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

        [HttpPost("update")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> UpdateUser ( UserUpdateRequestModel request)
        {
            try
            {
                int id = await _userService.UpdateUser(request);
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

        [HttpPost("desactive/user/{id}/owner/{owner_id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> DesactiveUser ( int id, int owner_id )
        {
            try
            {
                int user = await _userService.DesactiveUser(id, owner_id);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário desativado com sucesso.")
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

        [HttpPost("reactive/user/{id}/owner/{owner_id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<int>>> ReactiveUser ( int id , int owner_id)
        {
            try
            {
                int user = await _userService.ReactiveUser(id, owner_id);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Usuário reativado com sucesso.")
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

        [HttpPost("update-password/{password}/user/{id}/owner/{owner_id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<bool>>> UpdatePassword ( int id, string password, int owner_id )
        {
            try
            {
                var user = await _userService.UpdatePassword(password, id, owner_id);
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

        [HttpPost("send/email/{email}/user/{name}/{id}")]
        [ProducesResponseType(typeof(BaseResponse<EmailCodigoResponseModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<EmailCodigoResponseModel>), 400)]
        [ProducesResponseType(typeof(BaseResponse<EmailCodigoResponseModel>), 404)]
        [ProducesResponseType(typeof(BaseResponse<EmailCodigoResponseModel>), 500)]
        public async Task<ActionResult<BaseResponse<EmailCodigoResponseModel>>> SendCodeToEmail ( string email, string name, int id )
        {
            try
            {
                EmailCodigoResponseModel user = await _userService.SendCodeToEmail(email, name, id);
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
       
        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> GetUserById(int id )
        {
            try
            {
                var user = await _userService.GetUserById(id);
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
       
        
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> GetUserByName ( string name)
        {
            try
            {
                var user = await _userService.GetUserByName(name);
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
        
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> GetUserByEmail ( string email )
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);
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
        
        [HttpGet("type/{type}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UserResponseModel>>>> GetUserByType ( int type )
        {
            try
            {
                var user = await _userService.GetUserByType(type);
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
        
        [HttpGet("type/{type}/owner/{id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UserResponseModel>>>> GetUserByTypeAndOwnerId ( int type, int id )
        {
            try
            {
                var user = await _userService.GetUserByTypeAndOwnerId(type, id);
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
        
        [HttpGet("owner/{id}")]
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
        public async Task<ActionResult<BaseResponse<List<UserResponseModel>>>> GetUserByOwner ( int id )
        {
            try
            {
                var user = await _userService.GetUserByOwner(id);
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
    }
}
