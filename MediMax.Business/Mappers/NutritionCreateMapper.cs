using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class NutritionCreateMapper : Mapper<NutritionCreateRequestModel>, INutritionCreateMapper
    {
        private readonly Nutrition _alimentacao;

        public NutritionCreateMapper()
        {
            _alimentacao = new Nutrition();
        }

        public Nutrition GetFood()
        {
            _alimentacao.UserId = BaseMapping.UserId;
            _alimentacao.NutritionType = BaseMapping.tipo_refeicao;
            _alimentacao.Time = BaseMapping.horario;
            _alimentacao.NutritionDetailId = BaseMapping.detalhe_alimentacao_id;
            return _alimentacao;
        }
    }
}
