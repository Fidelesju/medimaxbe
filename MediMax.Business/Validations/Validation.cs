using FluentValidation;
using FluentValidation.Results;
using MediMax.Data.ApplicationModels;

namespace MediMax.Business.Validations
{
    public abstract class Validation<T> : AbstractValidator<T>
    {
        private Dictionary<string, string>? _errors;
        private ValidationResult? _validationResult;

        public Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        protected void SetErrors(Dictionary<string, string> errors)
        {
            _errors = errors;
        }

        public bool IsValid(T entity)
        {
            EvaluateErrors(entity);
            return _validationResult.IsValid;
        }

        protected bool ErrorsAreEmpty()
        {
            return _errors.Count == 0;
        }

        protected void EvaluateErrors(T entity)
        {
            _validationResult = Validate(entity);
            _errors = new Dictionary<string, string>();
            foreach (ValidationFailure validationFailure in _validationResult.Errors)
            {
                if (!_errors.Keys.Contains(validationFailure.PropertyName))
                {
                    _errors.Add(validationFailure.PropertyName, validationFailure.ErrorMessage);
                }
            }
        }

        protected void MergeError(Dictionary<string, string> errors, string objectName)
        {
            string key;
            string message;
            foreach (KeyValuePair<string, string> error in errors)
            {
                key = objectName + "." + error.Key;
                key = key.Substring(0, 1).ToLower() + key.Substring(1);
                message = error.Value;
                if (!_errors.Keys.Contains(key))
                {
                    _errors.Add(key, message);
                }
            }
        }

        protected abstract List<PersistenceError> GetPersistenceValidations();

        public Dictionary<string, string> GetPersistenceErrors(string exceptionMessage)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            bool errorFound = false;
            foreach (PersistenceError persistenceError in GetPersistenceValidations())
            {
                foreach (string key in persistenceError.Keys)
                {
                    if (exceptionMessage.Contains(key))
                    {
                        errors.Add(persistenceError.Field, persistenceError.Message);
                        errorFound = true;
                        break;
                    }
                }

                if (errorFound)
                {
                    break;
                }
            }

            return errors;
        }

        public Dictionary<string, string> GetPersistenceErrors(Exception exception)
        {
            string exceptionError;
            Dictionary<string, string> errors;

            exceptionError = exception.InnerException == null
                ? exception.Message
                : exception.InnerException.Message;

            errors = GetPersistenceErrors(exceptionError);
            if (errors.Count == 0)
            {
                throw exception;
            }
            return errors;
        }
    }
}