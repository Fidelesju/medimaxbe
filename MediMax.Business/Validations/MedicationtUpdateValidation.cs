using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class MedicationtUpdateValidation : Validation<MedicationUpdateRequestModel>
    {
        public MedicationtUpdateValidation()
        {
            validarRequestMedicamento();
        }

        private void validarRequestMedicamento()
        {
            RuleFor(u => u.medicine_name)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));

            RuleFor(u => u.expiration_date)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField);

            RuleFor(u => u.package_quantity)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .GreaterThan(0)
                .WithMessage("A quantidade da embalagem deve ser maior que zero.");

            RuleFor(u => u.dosage)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(0, 5)
                .WithMessage("A dosagem deve ser maior que zero.");

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