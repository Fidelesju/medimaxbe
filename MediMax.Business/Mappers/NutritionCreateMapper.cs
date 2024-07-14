using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class NutritionCreateMapper : Mapper<NutritionCreateRequestModel>, INutritionCreateMapper
    {
        private readonly Alimentacao _alimentacao;

        public NutritionCreateMapper()
        {
            _alimentacao = new Alimentacao();
        }

        public Alimentacao GetFood()
        {
            _alimentacao.UserId = BaseMapping.UserId;
            _alimentacao.tipo_refeicao = BaseMapping.tipo_refeicao;
            _alimentacao.horario = BaseMapping.horario;
            _alimentacao.detalhe_alimentacao_id = BaseMapping.detalhe_alimentacao_id;
            return _alimentacao;
        }
    }
}
