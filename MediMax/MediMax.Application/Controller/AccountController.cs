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
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IAccountService accountService,
            ILogger<AccountController> logger,
            ILoggerService loggerService
        ) : base(logger, loggerService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [AllowAnonymous, HttpPost("Login")]
        public async Task<ActionResult<BaseResponse<LoginResponseModel>>> Login(LoginRequestModel request)
        {
            LoginResponseModel loginResponse;
            BaseResponse<LoginResponseModel> response;
            try
            {
                loginResponse = await _accountService.AuthenticateUser(request);
                response = BaseResponse<LoginResponseModel>
                        .Builder()
                        .SetMessage("Usuário autenticado.")
                        .SetData(loginResponse)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException)
            {
                return StatusCode(401, BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Parametros incorretos");
                return StatusCode(401, "Email ou senha incorreto");
            }
        }

        [AllowAnonymous, HttpPost("Login/Admin")]
        public async Task<ActionResult<BaseResponse<LoginAdminResponseModel>>> LoginAdmin(LoginRequestModel loginRequest)
        {
            LoginAdminResponseModel loginResponse;
            BaseResponse<LoginAdminResponseModel> response;
            try
            {
                loginResponse = await _accountService.AuthenticateUserAdmin(loginRequest);
                response = BaseResponse<LoginAdminResponseModel>
                        .Builder()
                        .SetMessage("Usuário autenticado.")
                        .SetData(loginResponse)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException)
            {
                return StatusCode(401, BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        [AllowAnonymous, HttpPost("Login/Owner")]
        public async Task<ActionResult<BaseResponse<LoginOwnerResponseModel>>> LoginOwner(LoginRequestModel loginRequest)
        {
            LoginOwnerResponseModel loginResponse;
            BaseResponse<LoginOwnerResponseModel> response;
            try
            {
                loginResponse = await _accountService.AuthenticateUserOwner(loginRequest);
                response = BaseResponse<LoginOwnerResponseModel>
                    .Builder()
                    .SetMessage("Usuário autenticado.")
                    .SetData(loginResponse)
                ;
                return Ok(response);
            }
            catch (RecordNotFoundException)
            {
                return StatusCode(401, BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }


        //[Authorize(Roles = "owner, admin"), HttpPost("Owner/UpdatePassword")]
        //public async Task<ActionResult<BaseResponse<bool>>> UpdateOwnerPassword(OwnerUpdatePasswordRequestModel request)
        //{
        //    BaseResponse<bool> response;
        //    bool success;
        //    try
        //    {
        //        success = await _accountService.UpdateOwnerPassword(request);
        //        if (!success)
        //        {
        //            response = BaseResponse<bool>
        //                    .Builder()
        //                    .SetMessage("Credenciais inválidas.")
        //                    .SetData(false)
        //                ;
        //            return BadRequest(response);
        //        }
        //        response = BaseResponse<bool>
        //                .Builder()
        //                .SetMessage("Senha atualizada com sucesso.")
        //                .SetData(true)
        //            ;
        //        return Ok(response);
        //    }
        //    catch (CustomValidationException exception)
        //    {
        //        return ValidationErrorsBadRequest(exception);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await UntreatedException(exception);
        //    }
        //}

        //[AllowAnonymous, HttpPost("Owner/RecoverPassword")]
        //public async Task<ActionResult<BaseResponse<bool>>> RecoverOwnerPassword(OwnerSendForgotPasswordRequestModel request)
        //{
        //    BaseResponse<bool> response;
        //    bool success;
        //    try
        //    {
        //        success = await _accountService.RecoverOwnerPassword(request.Email);
        //        if (!success)
        //        {
        //            response = BaseResponse<bool>
        //                .Builder()
        //                .SetMessage("Erro ao enviar email.")
        //                .SetData(false)
        //            ;
        //            return BadRequest(response);
        //        }
        //        response = BaseResponse<bool>
        //            .Builder()
        //            .SetMessage("Senha nova enviada para seu email.")
        //            .SetData(true)
        //        ;
        //        return Ok(response);
        //    }
        //    catch (CustomValidationException exception)
        //    {
        //        return ValidationErrorsBadRequest(exception);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await UntreatedException(exception);
        //    }
        //}
    }
}