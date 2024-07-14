using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class TreatmentManagementCreateValidation : Validation<GerencimentoTreatmentCreateRequestModel>
    {
        public TreatmentManagementCreateValidation()
        {
            ValidarSeMedicamentoFoiTomado();
            ValidarHorarioCorreto();
            ValidarHorarioTomado();
        }

        private void ValidarHorarioCorreto()
        {
            RuleFor(u => u.horario_correto_Treatment)
                 .NotEmpty().WithMessage(DefaultErrorMessages.RequiredField)
                 .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                 .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));
        } 
        
        private void ValidarHorarioTomado()
        {

            RuleFor(u => u.horario_ingestao_medication)
                .NotEmpty().WithMessage(DefaultErrorMessages.RequiredField)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));
        } 
        private void ValidarSeMedicamentoFoiTomado()
        {

             RuleFor(u => u.foi_tomado)
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