using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITratamentoDb
    {
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name, int userId );
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime, int userId );
        Task<bool> AlterandoTratamento(int remedio_id,
                string nome,
                int quantidade_medicamentos,
                string horario_inicio,
                int intervalo_tratamento,
                int tempo_tratamento_dias,
                string recomendacoes_alimentacao,
                string observacao,
                int id);
        Task<bool> DeletandoTratamento(int id);
        Task<List<TratamentoResponseModel>> BuscarTodosTratamentoAtivos ( int userId );
        Task<TratamentoResponseModel> BuscarTratamentoPorId ( int treatmentId, int userId );
        Task<TratamentoResponseModel> BuscarTratamentoPorIdParaStatus ( int treatmentId, int userId );
        Task<List<TratamentoResponseModel>> BuscarTodosTratamentoInativos ( int userId );
    }
}
