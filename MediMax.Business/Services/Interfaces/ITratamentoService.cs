using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface ITratamentoService
    {
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name);
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime);
        Task<bool> DeletandoTratamento(int id);
        Task<List<TratamentoResponseModel>> BuscarTodosTratamentoAtivos ( );
        Task<TratamentoResponseModel> BuscarTratamentoPorId ( int treatmentId );
    }
}
