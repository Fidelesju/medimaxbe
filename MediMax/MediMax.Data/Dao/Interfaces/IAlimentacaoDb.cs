using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IAlimentacaoDb
    {
        Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals);
    }
}
