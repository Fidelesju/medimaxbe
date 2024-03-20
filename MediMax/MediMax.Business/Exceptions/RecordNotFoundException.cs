namespace MediMax.Business.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base("Registro não encontrado.")
        {
        }

        public RecordNotFoundException(string message) : base(message)
        {
        }

        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
