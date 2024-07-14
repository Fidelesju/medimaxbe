using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class DispenserStatusCreateValidation : Validation<DispenserStatusCreateRequestModel>
    {
        public DispenserStatusCreateValidation()
        {
            validarRequestStatusDispense();
        }

        private void validarRequestStatusDispense()
        {
            RuleFor(u => u.medicamento_id)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField);

            RuleFor(u => u.Treatment_id)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField);

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