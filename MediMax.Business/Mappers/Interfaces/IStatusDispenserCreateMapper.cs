using MediMax.Business.Mappers.Interface;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers.Interfaces
{
    public interface IStatusDispenserCreateMapper : IMapper<StatusDispenserCreateRequestModel>
    {
        StatusDispenser BuscarStatusDispenser();
    }
}
