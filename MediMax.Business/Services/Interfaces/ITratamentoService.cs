using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface ITratamentoService
    {
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name, int userId );
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime, int userId );
        Task<bool> DeletandoTratamento(int id);
        Task<List<TratamentoResponseModel>> BuscarTodosTratamentoAtivos ( int userId );
        Task<TratamentoResponseModel> BuscarTratamentoPorId ( int treatmentId, int userId );
    }
}
