using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Application.Controller
{
    [Route("[controller]"), ApiController]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;
        public UserController(
            ILogger<UserController> logger,
            ILoggerService loggerService,
            IUserService userService) : base(logger, loggerService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> CreateUsers(UserCreateRequestModel request)
        {
            BaseResponse<int> response;
            int id;
            try
            {
                id = await _userService.CreateUsers(request);
                if (id == 0)
                {
                    response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Falha ao cadastrar um usuário.")
                        .SetData(0)
                    ;
                    return BadRequest(response);
                }

                response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Usuário criado com sucesso.")
                        .SetData(id)
                    ;
                return Ok(response);
            }
            catch (CustomValidationException exception)
            {
                return ValidationErrorsBadRequest(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("UserId/{userId}")]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> GetUserById(int userId)
        {
            UserResponseModel user;
            BaseResponse<UserResponseModel> response;
            try
            {
                user = await _userService.GetUserById(userId);
                response = BaseResponse<UserResponseModel>
                        .Builder()
                        .SetMessage("Usuário encontrado com sucesso.")
                        .SetData(user)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("Login/{email}/{password}")]
        public async Task<ActionResult<BaseResponse<UserResponseModel>>> GetUserByEmail(string email)
        {
            UserResponseModel user;
            BaseResponse<UserResponseModel> response;
            try
            {
                user = await _userService.GetUserByEmail(email);
                response = BaseResponse<UserResponseModel>
                        .Builder()
                        .SetMessage("Usuário encontrado com sucesso.")
                        .SetData(user)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }
    }
}
