using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class NutritionCreateValidation : Validation<NutritionCreateRequestModel>
    {
        public NutritionCreateValidation()
        {
            ValidateAlimentacao();
        }

        private void ValidateAlimentacao()
        {
            RuleFor(n => n.detalhe_alimentacao[0].alimento)
            .NotEmpty()
            .WithMessage(DefaultErrorMessages.RequiredField)
            .Length(3, 150)
            .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
            .WithName("Alimento deve conter mais de 3 caracteres!");
        }

        private void ValidateQuantidade()
        {
           
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