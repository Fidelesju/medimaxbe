using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class DetalheAlimentacaoCreateMapper : Mapper<DetalheAlimentacaoCreateRequestModel>, IDetalheAlimentacaoCreateMapper
    {
        private readonly NutritionDetail? _detalheAlimentacao;

        public DetalheAlimentacaoCreateMapper ( )
        {
            _detalheAlimentacao = new NutritionDetail();
        }

        public NutritionDetail GetFoodDetail ( DetalheAlimentacaoCreateRequestModel request)
        {
            _detalheAlimentacao.Nutrition = request.alimento;
            _detalheAlimentacao.Quantity = request.quantidade;
            _detalheAlimentacao.UnitMeasurement = request.unidade_medida;
            return _detalheAlimentacao;
        }
    }
}
