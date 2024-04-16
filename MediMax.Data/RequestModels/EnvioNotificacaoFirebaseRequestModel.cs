namespace MediMax.Data.RequestModels
{
    public class EnvioNotificacaoFirebaseRequestModel<T> : where T: class
    {
        public class NotificationModel
        {
            public string title { get; set; }
            public string body { get; set; }
            public string subTitle { get; set; }
            public string icon { get; set; }
        }
        public string to { get; set; }
        public NotificationModel notification { get; set; }
        public T data { get; set; }

        public static EnvioNotificacaoFirebaseRequestModel<T> Builder()
        {
            return new EnvioNotificacaoFirebaseRequestModel<T>()
            {
                notification = new NotificationModel()
            };
        }

        public EnvioNotificacaoFirebaseRequestModel<T> SetTitle(string title)
        {
            notification.title = title;
            return this;
        }
        public EnvioNotificacaoFirebaseRequestModel<T> SetBody(string body)
        {
            notification.body = body;
            return this;
        }
        public EnvioNotificacaoFirebaseRequestModel<T> SetSubTitle(string subTitle)
        {
            notification.subTitle = subTitle;
            return this;
        }
        public EnvioNotificacaoFirebaseRequestModel<T> SetIcon(string icon)
        {
            notification.icon = icon;
            return this;
        }
        public EnvioNotificacaoFirebaseRequestModel<T> SetTo(string to)
        {
            this.to = to;
            return this;
        }
        public EnvioNotificacaoFirebaseRequestModel<T> SetData(T data)
        {
            this.data = data;
            return this;
        }
    }
}
