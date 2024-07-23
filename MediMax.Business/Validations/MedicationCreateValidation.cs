using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class MedicationCreateValidation : Validation<MedicationCreateRequestModel>
    {
        public MedicationCreateValidation()
        {
            validarRequestMedicamento();
        }

        private void validarRequestMedicamento()
        {
            RuleFor(u => u.name_medication)
                .NotEmpty()
                .WithMessage("O nome do medicamento é obrigatório")
                .Length(3, 150)
                .WithMessage("O nome do medicamento deve ter entre 3 e 150 caracteres");

            RuleFor(u => u.package_quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade na embalagem deve ser maior que zero");

            RuleFor(u => u.dosage)
                .Length(0, 5)
                .WithMessage("A dosagem deve ser maior que zero");
        }
        private bool BeAValidDate ( string date )
        {
            return DateTime.TryParse(date, out DateTime _);
        }

        private bool BeNotNearExpiration ( string date )
        {
            if (DateTime.TryParse(date, out DateTime expirationDate))
            {
                // Define quantos dias considera como "próximo do vencimento"
                int daysToBeConsideredNear = 30;
                var daysUntilExpiration = (expirationDate - DateTime.Now).TotalDays;
                return daysUntilExpiration > daysToBeConsideredNear;
            }
            return false;
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