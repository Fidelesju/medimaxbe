using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class AlimentacaoCreateValidation : Validation<AlimentacaoCreateRequestModel>
    {
        public AlimentacaoCreateValidation()
        {
            ValidateAlimentacao();
            ValidateQuantidade();
        }

        private void ValidateAlimentacao()
        {
            RuleFor(food => food.alimento)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(food => CustomValidations.IsInLengthInterval(3, 150, food))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                ;
        }

        private void ValidateQuantidade()
        {
            RuleFor(u => u.quantidade)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
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