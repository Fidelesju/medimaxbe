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
        private readonly IUserService _userService;
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
        [ProducesResponseType(typeof(BaseResponse<int>), 200)]
        [ProducesResponseType(typeof(BaseResponse<int>), 400)]
        [ProducesResponseType(typeof(BaseResponse<int>), 404)]
        [ProducesResponseType(typeof(BaseResponse<int>), 500)]
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
            catch (RecordNotFoundException ex)
            {
                _logger.LogError(ex, "Login: Controller");
                return Unauthorized(BaseResponse<string>
                    .Builder()
                    .SetMessage("Acesso não autorizado.")
                    .SetData("")
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login: Controller");
                return HandleException(ex);
            }
        }

        private ActionResult HandleException(Exception exception)
        {
            _logger.LogError(exception, "Parametros incorretos");
            return StatusCode(401, "Email ou senha incorreto");
        }
    }
}
