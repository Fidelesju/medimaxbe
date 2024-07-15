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
            RuleFor(u => u.Name_User)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("Nome de usuário invalido");
        }

        private void ValidateEmail()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .EmailAddress()
                .WithMessage(DefaultErrorMessages.InvalidEmail)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("E-mail");
        }

        private void ValidatePassword()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(p => CustomValidations.ValidatePasswordStrength(p))
                .WithMessage(DefaultErrorMessages.PasswordOutFormat())
                .WithName("Senha");
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