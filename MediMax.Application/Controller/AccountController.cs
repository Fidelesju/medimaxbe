using MediMax.Application.Controller;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
            _accountService = accountService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<BaseResponse<LoginResponseModel>>> Login(LoginRequestModel request)
        {
            try
            {
                var loginResponse = await _accountService.AuthenticateUser(request);
                return Ok(BaseResponse<LoginResponseModel>
                        .Builder()
                        .SetMessage("Usuário autenticado.")
                        .SetData(loginResponse)
                    );
            }
            catch (RecordNotFoundException)
            {
                return Unauthorized(BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login/Admin")]
        public async Task<ActionResult<BaseResponse<LoginAdminResponseModel>>> LoginAdmin(LoginRequestModel loginRequest)
        {
            try
            {
                var loginResponse = await _accountService.AuthenticateUserAdmin(loginRequest);
                return Ok(BaseResponse<LoginAdminResponseModel>
                        .Builder()
                        .SetMessage("Usuário autenticado.")
                        .SetData(loginResponse)
                    );
            }
            catch (RecordNotFoundException)
            {
                return Unauthorized(BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login/Owner")]
        public async Task<ActionResult<BaseResponse<LoginOwnerResponseModel>>> LoginOwner(LoginRequestModel loginRequest)
        {
            try
            {
                var loginResponse = await _accountService.AuthenticateUserOwner(loginRequest);
                return Ok(BaseResponse<LoginOwnerResponseModel>
                    .Builder()
                    .SetMessage("Usuário autenticado.")
                    .SetData(loginResponse)
                );
            }
            catch (RecordNotFoundException)
            {
                return Unauthorized(BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        private ActionResult HandleException(Exception exception)
        {
            _logger.LogError(exception, "Parametros incorretos");
            return StatusCode(401, "Email ou senha incorreto");
        }
    }
}
