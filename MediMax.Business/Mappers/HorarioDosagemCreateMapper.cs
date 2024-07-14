using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class HorarioDosagemCreateMapper : Mapper<HorariosDosagemCreateRequestModel>, IHorarioDosagemCreateMapper
    {
        public TimeDosage BuscarHorariosDosagem(HorariosDosagemCreateRequestModel request)
        {
            TimeDosage horarioDosagem = new TimeDosage(); // Criar uma nova instância
            horarioDosagem.TreatmentId = request.tratamento_id;
            horarioDosagem.Time = request.horario_dosagem;
            return horarioDosagem;
        }

    }
}
