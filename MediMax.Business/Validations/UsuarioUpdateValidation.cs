using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class UsuarioUpdateValidation : Validation<UsuarioUpdateRequestModel>
    {
        public UsuarioUpdateValidation ( )
        {
            ValidateEmail();
            ValidateName();
        }

        private void ValidateName()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("Nome de usuário");
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