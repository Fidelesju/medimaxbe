using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class StatusDispenserCreateValidation : Validation<StatusDispenserCreateRequestModel>
    {
        public StatusDispenserCreateValidation()
        {
            validarRequestStatusDispense();
        }

        private void validarRequestStatusDispense()
        {
            RuleFor(u => u.medicamento_id)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField);

            RuleFor(u => u.tratamento_id)
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