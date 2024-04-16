using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.RealTimeServices.Interfaces;
using System.Text.Json;
using ServiceStack;
using JsonSerializer = ServiceStack.Text.JsonSerializer;
using MediMax.Data.RequestModels;

namespace MediMax.Business.RealTimeServices
{
    public class FirebaseEnvioNotificacaoService : IFirebaseEnvioNotificacaoService
    {

        private readonly ILoggerService _loggerService;

        public FirebaseEnvioNotificacaoService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        // public async Task<bool> UpdateUserNotificationToken(int userId, string notificationToken)
        // {
        //     throw new System.NotImplementedException();
        // }

        public async Task<bool> SendPushNotificationToUser<T>(EnvioNotificacaoFirebaseRequestModel<T> notification) where T : class
        {
            //            HttpRequestMessage httpRequestMessage;

            //            HttpClient httpClient;
            // string json;
            //          httpClient = new HttpClient();
            // json = JsonSerializer.SerializeToString(notification);
            // string response;
            // notificationToken = notification.Token;
            // message = new Message();
            // message.Data = new Dictionary<string, string>{{"test-key", "test-data"}};
            // message.Token = notificationToken;
            // FirebaseApp firebaseApp = FirebaseApp.Create();
            // FirebaseMessaging firebase = FirebaseMessaging.GetMessaging(firebaseApp);
            // response = "";
            // try
            // {
            //     response = await firebase.SendAsync(message);
            //     await _loggerService.LogInfo($@"A message with id {response} was sent to token {notificationToken}");
            //     Console.WriteLine($@"A message with id {response} was sent to token {notificationToken}");
            // }
            // catch (ArgumentException exception)
            // {
            //     Console.Write($@"Firebase ArgumentException exception thrown for notification for token {notificationToken}");
            //     Console.WriteLine(exception.Message);
            //     await _loggerService.LogErrorServicesBackground(exception);
            // }
            // catch (FirebaseMessagingException exception)
            // {
            //     Console.Write($@"Firebase FirebaseMessagingException exception thrown for notification for token {notificationToken}");
            //     Console.WriteLine(exception.Message);
            //     await _loggerService.LogErrorServicesBackground(exception);
            // }
            // return response != "";
            //string json = JsonSerializer.SerializeToString(notification);
            //string end = @"https://fcm.googleapis.com/fcm/send";
            //string response = await end.PostToUrlAsync(json, $@"*/*",
            //    request =>
            //    {
            //        request.Headers["Content-Type"] = "application/json";
            //        request.Headers["Authorization"] =
            //            $@"key=AAAAqTMkFWo:APA91bFPOuHjKr2iV2TRxETv_bG--9a-Q-WOri3BLNdguINg3KETuGSIf_tvX4CGb-X65r8qwpD3Snje6HARLFcZmyEM9vjfeS1ljofgmYWbXw0HKCoA5RAVMuJawb73AfYOnSBj_39K";
            //    });
            //await _loggerService.LogInfo($"Push notification sent. Request: {json}, Response: {response}");
            // Console.WriteLine(response);

            // JsonSerializer<ModelSample>.Deserializer(model);
            // end.PostToUrl(requestFilter:webReq =>{
            // webReq.Headers["X-Api-Key"] = apiKey;
            // }, json);

            return true;
        }
    }
}
