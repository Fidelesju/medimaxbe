using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class UserCreateValidation : Validation<UserCreateRequestModel>
    {
        public UserCreateValidation()
        {
            ValidateEmail();
            ValidateName();
            ValidatePassword();
        }

        private void ValidateName()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateEmail()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .EmailAddress()
                .WithMessage(DefaultErrorMessages.InvalidEmail)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidatePassword()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(p => CustomValidations.ValidatePasswordStrength(p))
                .WithMessage(DefaultErrorMessages.PasswordOutFormat())
                ;
        }

        protected override List<PersistenceError> GetPersistenceValidations()
        {
            return new List<PersistenceError>
            {
                new PersistenceError
                {
                }
            };
        }
    }
}