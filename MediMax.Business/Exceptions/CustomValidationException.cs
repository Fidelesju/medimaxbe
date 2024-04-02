using System.Collections.Generic;

namespace MediMax.Business.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(Dictionary<string, string> errors)
        {
            Errors = errors;
        }

        public CustomValidationException(string message) : base(message)
        {
            IsStringError = true;
        }

        public CustomValidationException(List<Dictionary<string, string>> errorsList)
        {
            ErrorsList = errorsList;
        }

        public Dictionary<string, string> Errors { get; set; }
        public List<Dictionary<string, string>> ErrorsList { get; set; }
        public bool IsStringError { get; set; }
        public object Failures { get; set; }
    }
}