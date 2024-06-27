using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class NotificacaoController : BaseController<NotificacaoController>
    {
        private readonly ILoggerService _loggerService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationService _notificacaoService; // Certifique-se de que esse serviço esteja definido e injetado

        public NotificacaoController (
            ILogger<NotificacaoController> logger,
            ILoggerService loggerService,
            IHubContext<NotificationHub> hubContext,
            INotificationService notificacaoService ) : base(logger, loggerService)
        {
            _hubContext = hubContext;
            _notificacaoService = notificacaoService;
        }

        [HttpPost("Create/{userId}/{message}")]
        public async Task<ActionResult<BaseResponse<int>>> NotifyUserAsync ( int userId, string message )
        {
            try
            {
                int id = await _notificacaoService.NotifyUserAsync(userId, message);
                var response = BaseResponse<int>.Builder()
                    .SetMessage("Notificação criada com sucesso.")
                    .SetData(id);

                // Enviar notificação via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Uma nova notificação foi criada!");

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

    }
}
