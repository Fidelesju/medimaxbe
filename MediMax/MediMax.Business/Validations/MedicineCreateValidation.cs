using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class MedicineCreateValidation : Validation<MedicamentoETratamentoCreateRequestModel>
    {
        public MedicineCreateValidation()
        {
            ValidateName();
        }

        private void ValidateName()
        {
            RuleFor(u => u.nome)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
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