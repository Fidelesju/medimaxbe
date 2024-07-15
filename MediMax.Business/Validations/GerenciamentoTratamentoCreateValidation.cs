using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class TreatmentManagementCreateValidation : Validation<TreatmentManagementCreateRequestModel>
    {
        public TreatmentManagementCreateValidation()
        {
            ValidarSeMedicamentoFoiTomado();
            ValidarHorarioCorreto();
            ValidarHorarioTomado();
        }

        private void ValidarHorarioCorreto()
        {
            RuleFor(u => u.Correct_Time_Treatment)
                 .NotEmpty().WithMessage(DefaultErrorMessages.RequiredField)
                 .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                 .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));
        } 
        
        private void ValidarHorarioTomado()
        {

            RuleFor(u => u.Medication_Intake_Time)
                .NotEmpty().WithMessage(DefaultErrorMessages.RequiredField)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));
        } 
        private void ValidarSeMedicamentoFoiTomado()
        {

             RuleFor(u => u.Was_Taken)
                .NotNull().WithMessage(DefaultErrorMessages.RequiredField);
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