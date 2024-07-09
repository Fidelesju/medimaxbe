using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class DetalheAlimentacaoCreateMapper : Mapper<DetalheAlimentacaoCreateRequestModel>, IDetalheAlimentacaoCreateMapper
    {
        private readonly DetalheAlimentacao? _detalheAlimentacao;

        public DetalheAlimentacaoCreateMapper ( )
        {
            _detalheAlimentacao = new DetalheAlimentacao();
        }

        public DetalheAlimentacao GetFoodDetail ( DetalheAlimentacaoCreateRequestModel request)
        {
            _detalheAlimentacao.alimento = request.alimento;
            _detalheAlimentacao.quantidade = request.quantidade;
            _detalheAlimentacao.unidade_medida = request.unidade_medida;
            return _detalheAlimentacao;
        }
    }
}
