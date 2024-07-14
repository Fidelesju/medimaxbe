using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class TreatmentManagementCreateMapper : Mapper<GerencimentoTreatmentCreateRequestModel>, ITreatmentManagementCreateMapper
    {
        private readonly TreatmentManagement? _TreatmentManagement;

        public TreatmentManagementCreateMapper()
        {
            _TreatmentManagement = new TreatmentManagement();
        }

        public TreatmentManagement GetTreatmentManagement()
        {
            _TreatmentManagement.Treatment_id = BaseMapping.Treatment_id;
            _TreatmentManagement.horario_correto_Treatment = BaseMapping.horario_correto_Treatment;
            _TreatmentManagement.horario_ingestao_medication = BaseMapping.horario_ingestao_medication;
            _TreatmentManagement.data_ingestao_medication = BaseMapping.data_ingestao_medication;
            _TreatmentManagement.foi_tomado = BaseMapping.foi_tomado;
            return _TreatmentManagement;
        }
    }
}
