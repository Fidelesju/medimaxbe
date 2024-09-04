using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Data.ApplicationModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.SignalR;
using MimeKit;

namespace MediMax.Business.RealTimeServices
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService ( IHubContext<NotificationHub> hubContext )
        {
            _hubContext = hubContext;
        }

        public async Task<int> NotifyUserAsync ( int userId, string message )
        {
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message);
            return userId;
        }

        public async Task<bool> SendNotificationToEmail ( string email, string title, string subjectEmail , string bodyEmail )
        {
            //TODO Criando tabela de notificação
            //TODO Adicionar informações na tabela
            Boolean response;
            string subject = $"{subjectEmail}";
            string body = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Código de Verificação</title>
                <style>
                    body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }}
                    .container {{ background-color: #ffffff; width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }}
                    .header {{ background-color: #004a99; color: #ffffff; padding: 10px 20px; text-align: center; }}
                    .body {{ padding: 20px; text-align: center; line-height: 1.6; }}
                    .footer {{ font-size: 12px; text-align: center; color: #777; padding: 20px; }}
                    .code {{ font-size: 24px; color: #333333; font-weight: bold; margin: 20px 0; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>{title}</div>
                    <div class='body'>
                        <p>{bodyEmail},</p>
                    </div>
                    <div class='footer'>© 2024 Medimax. Todos os direitos reservados.</div>
                </div>
            </body>
            </html>";

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("MediMax", "suportemedimax@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("suportemedimax@gmail.com", "maodxmtcbrxgomhy");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                response = true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return response;
        }

    }
}