namespace MediMax.Data.ApplicationModels
{
    public class PersistenceError
    {
        public string[] Keys { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }
}