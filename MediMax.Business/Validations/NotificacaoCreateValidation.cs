using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class NotificacaoCreateValidation : Validation<NotificacaoCreateRequestModel>
    {
        public NotificacaoCreateValidation()
        {
            ValidateDescricao();
            ValidateTitulo();
            ValidateHorario();
        }

        private void ValidateTitulo()
        {
            RuleFor(u => u.titulo)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("Titulo");
        }

        private void ValidateHorario()
        {
            RuleFor(u => u.horario)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("Horario");
        }

        private void ValidateDescricao()
        {
            RuleFor(u => u.descricao)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("Descrição");
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