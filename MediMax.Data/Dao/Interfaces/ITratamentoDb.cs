using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITratamentoDb
    {
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name);
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime);
    }
}
