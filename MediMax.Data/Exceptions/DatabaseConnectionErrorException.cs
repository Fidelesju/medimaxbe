namespace MediMax.Data.Exceptions
{
    public class DatabaseConnectionErrorException : Exception
    {
        public DatabaseConnectionErrorException(string message, Exception inner) : base(message, inner)
        {
            Console.WriteLine("Failure on connection to database.");
        }
    }
}