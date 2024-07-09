using FluentValidation;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Validations
{
    public class AlimentacaoCreateValidation : Validation<AlimentacaoCreateRequestModel>
    {
        public AlimentacaoCreateValidation()
        {
        }

        private void ValidateAlimentacao()
        {
        }

        private void ValidateQuantidade()
        {
           
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