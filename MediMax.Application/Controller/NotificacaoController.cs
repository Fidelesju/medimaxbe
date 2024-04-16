using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class NotificacaoController : BaseController<NotificacaoController>
    {
        private readonly INotificacaoService _notificacaoService;
        private readonly IFirebaseEnvioNotificacaoService _firebaseEnvioNotificacaoService;
        private readonly ILoggerService _loggerService;

        public NotificacaoController(
            ILogger<NotificacaoController> logger,
            ILoggerService loggerService,
            INotificacaoService notificacaoService,
            IFirebaseEnvioNotificacaoService firebaseEnvioNotificacaoService) : base(logger, loggerService)
        {
            _notificacaoService = notificacaoService;
            _firebaseEnvioNotificacaoService = firebaseEnvioNotificacaoService;

        }

        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> RegistarNotificacao(NotificacaoCreateRequestModel request)
        {
            try
            {
                int id = await _notificacaoService.RegistarNotificacao(request);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Notificação criada com sucesso.")
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

        [HttpPost("Send")]
        public async Task<ActionResult<BaseResponse<string>>> EnviaNotificacao(EnvioNotificacaoFirebaseRequestModel<ProfileCreateRequestModel> request)
        {
            BaseResponse<string> response;
            bool success;
            try
            {
                success = await _firebaseEnvioNotificacaoService.SendPushNotificationToUser(request);
                if (!success)
                {
                    response = BaseResponse<string>
                        .Builder()
                        .SetMessage("Falha no envio da notificação.")
                        .SetData("")
                    ;
                    return BadRequest(response);
                }
                response = BaseResponse<string>
                    .Builder()
                    .SetMessage("Notificação enviada com sucesso.")
                    .SetData("")
                ;

                await _loggerService.LogInfo($"Notificação enviada com sucesso. Request: {request}, Response: {response}");
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                await _loggerService.LogInfo($"Erro ao enviar notificação. Request: {request}");
                return await NotFoundResponse(exception);

            }
            catch (Exception exception)
            {
                await _loggerService.LogInfo($"Erro ao enviar notificação. Request: {request}");
                return await UntreatedException(exception);
            }
        }
    }
}
