using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class TreatmentManagementCreateMapper : Mapper<GerencimentoTreatmentCreateRequestModel>, ITreatmentManagementCreateMapper
    {
        private readonly TreatmentManagement _TreatmentManagement;

        public TreatmentManagementCreateMapper()
        {
            _TreatmentManagement = new TreatmentManagement();
        }

        public TreatmentManagement GetTreatmentManagement()
        {
            _TreatmentManagement.TreatmentId = BaseMapping.Treatment_id;
            _TreatmentManagement.CorrectTimeTreatment = BaseMapping.horario_correto_Treatment;
            _TreatmentManagement.MedicationIntakeTime = BaseMapping.horario_ingestao_medication;
            _TreatmentManagement.MedicationIntakeDate = BaseMapping.data_ingestao_medication;
            _TreatmentManagement.WasTaken = BaseMapping.foi_tomado;
            return _TreatmentManagement;
        }
    }
}
