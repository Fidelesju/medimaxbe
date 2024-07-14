using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class TreatmentCreateValidation : Validation<TreatmentCreateRequestModel>
    {
        public TreatmentCreateValidation ( )
        {
            ValidateName();
            ValidateDietaryRecommedations();
            ValidateObservation();
            ValidateTreatmentStartTime();
            ValidateTreatmentIntervalHours();
            ValidateTreatmentIntervalDays();

        }
        private void ValidateName ( )
        {
            RuleFor(t => t.medicine_name)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Length(3, 150)
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150))
                .WithName("Nome de medicamento");
        }

         private void ValidateDietaryRecommedations( )
        {
            RuleFor(u => u.dietary_recommendations)
                .MaximumLength(255)
                .WithMessage("As recomendações de alimentação devem ter no máximo 255 caracteres.");
        }
        
        private void ValidateObservation( )
        {
            RuleFor(u => u.observation)
                .MaximumLength(255)
                .WithMessage("As observações devem ter no máximo 255 caracteres.");
        }
        
        private void ValidateTreatmentStartTime( )
        {
            RuleFor(u => u.treatment_start_time)
              .NotEmpty()
              .WithMessage(DefaultErrorMessages.RequiredField);
        }
        private void ValidateTreatmentIntervalHours( )
        {
            RuleFor(u => u.treatment_interval_hours)
              .NotEmpty()
              .WithMessage(DefaultErrorMessages.RequiredField);
        }
        private void ValidateTreatmentIntervalDays( )
        {
            RuleFor(u => u.treatment_interval_hours)
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