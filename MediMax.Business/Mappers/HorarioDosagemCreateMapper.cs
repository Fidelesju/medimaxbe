using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class HorarioDosagemCreateMapper : Mapper<HorariosDosagemCreateRequestModel>, IHorarioDosagemCreateMapper
    {
        public HorariosDosagem BuscarHorariosDosagem(HorariosDosagemCreateRequestModel request)
        {
            HorariosDosagem horarioDosagem = new HorariosDosagem(); // Criar uma nova instância
            horarioDosagem.tratamento_id = request.tratamento_id;
            horarioDosagem.horario_dosagem = request.horario_dosagem;
            return horarioDosagem;
        }

    }
}
