using MediMax.Business.CoreServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class NotificacaoController : BaseController<NotificacaoController>
    {
        private readonly ILoggerService _loggerService;

        public NotificacaoController(
            ILogger<NotificacaoController> logger,
            ILoggerService loggerService) : base(logger, loggerService)
        {

        }

        //[HttpPost("Create")]
        //public async Task<ActionResult<BaseResponse<int>>> RegistarNotificacao(NotificacaoCreateRequestModel request)
        //{
        //    try
        //    {
        //        int id = await _notificacaoService.RegistarNotificacao(request);
        //        var response = BaseResponse<int>.Builder()
        //            .SetMessage("Notificação criada com sucesso.")
        //            .SetData(id);
        //        return Ok(response);
        //    }
        //    catch (CustomValidationException ex)
        //    {
        //        return ValidationErrorsBadRequest(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return await UntreatedException(ex);
        //    }
        //}

    }
}
