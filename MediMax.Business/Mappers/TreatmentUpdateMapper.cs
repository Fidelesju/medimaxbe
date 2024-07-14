using MediMax.Business.Mappers.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class TreatmentUpdateMapper : Mapper<TreatmentUpdateRequestModel>, ITreatmentUpdateMapper
    {
        private readonly Treatment? _treatment;

        public TreatmentUpdateMapper ( )
        {
            _treatment = new Treatment();
        }

        public Treatment GetTreatment()
        {
            _treatment.Id = BaseMapping.treatment_id;
            _treatment.MedicationQuantity = BaseMapping.medication_quantity;
            _treatment.StartTime = BaseMapping.treatment_start_time;
            _treatment.TreatmentIntervalHours = BaseMapping.treatment_interval_hours;
            _treatment.TreatmentIntervalDays= BaseMapping.treatment_interval_days;
            _treatment.DietaryRecommendations = BaseMapping.dietary_recommendations;
            _treatment.Observation = BaseMapping.observation;
            _treatment.IsActive = 1;
            _treatment.MedicationId = BaseMapping.medication_id;
            _treatment.NameMedication = BaseMapping.medicine_name;
            return _treatment;
        }
    }
}
