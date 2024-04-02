using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class GerenciamentoTratamentoCreateValidation : Validation<GerencimentoTratamentoCreateRequestModel>
    {
        public GerenciamentoTratamentoCreateValidation()
        {
            ValidarSeMedicamentoFoiTomado();
            ValidarHorarioCorreto();
            ValidarHorarioTomado();
        }

        private void ValidarHorarioCorreto()
        {
            RuleFor(u => u.horario_correto_tratamento)
                 .NotEmpty().WithMessage(DefaultErrorMessages.RequiredField)
                 .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                 .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));
        } 
        
        private void ValidarHorarioTomado()
        {

            RuleFor(u => u.horario_ingestao_medicamento)
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