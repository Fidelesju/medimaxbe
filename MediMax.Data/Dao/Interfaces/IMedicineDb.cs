using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IMedicineDb
    {
        Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos();
    }
}
