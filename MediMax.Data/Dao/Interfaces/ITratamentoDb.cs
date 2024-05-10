using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITratamentoDb
    {
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string name);
        Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime);
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
        Task<List<TratamentoResponseModel>> BuscarTodosTratamentoAtivos ( );
        Task<TratamentoResponseModel> BuscarTratamentoPorId ( int treatmentId );
        Task<TratamentoResponseModel> BuscarTratamentoPorIdParaStatus ( int treatmentId );
        Task<List<TratamentoResponseModel>> BuscarTodosTratamentoInativos ( );
    }
}
