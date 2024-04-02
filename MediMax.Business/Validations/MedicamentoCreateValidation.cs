using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class MedicamentoCreateValidation : Validation<MedicamentoETratamentoCreateRequestModel>
    {
        public MedicamentoCreateValidation()
        {
            validarRequestMedicamento();
        }

        private void validarRequestMedicamento()
        {
            RuleFor(u => u.nome)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .Must(name => CustomValidations.IsInLengthInterval(3, 150, name))
                .WithMessage(DefaultErrorMessages.TextOutOfBounds(3, 150));

            RuleFor(u => u.data_vencimento_medicamento)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField);

            RuleFor(u => u.quantidade_embalagem)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .GreaterThan(0)
                .WithMessage("A quantidade da embalagem deve ser maior que zero.");

            RuleFor(u => u.dosagem)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .GreaterThan(0)
                .WithMessage("A dosagem deve ser maior que zero.");

            RuleFor(u => u.quantidade_medicamento_por_dia)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .GreaterThan(0)
                .WithMessage("A quantidade de medicamento por dia deve ser maior que zero.");

            RuleFor(u => u.horario_inicial_tratamento)
                .NotEmpty()
                .WithMessage(DefaultErrorMessages.RequiredField);

            RuleFor(u => u.intervalo_tratamento_horas)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .GreaterThan(0)
                .WithMessage("O intervalo de tratamento em horas deve ser maior que zero.");

            RuleFor(u => u.intervalo_tratamento_dias)
                .NotNull()
                .WithMessage(DefaultErrorMessages.RequiredField)
                .GreaterThan(0)
                .WithMessage("O intervalo de tratamento em dias deve ser maior que zero.");

            RuleFor(u => u.recomendacoes_alimentacao)
                .MaximumLength(255)
                .WithMessage("As recomendações de alimentação devem ter no máximo 255 caracteres.");

            RuleFor(u => u.observacao)
                .MaximumLength(255)
                .WithMessage("A observação deve ter no máximo 255 caracteres.");
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