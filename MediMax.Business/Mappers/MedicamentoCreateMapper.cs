using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class MedicamentoCreateMapper : Mapper<MedicationCreateRequestModel>, IMedicationCreateMapper
    {
        private readonly Medication? _medications;

        public MedicamentoCreateMapper()
        {
            _medications = new Medication();
        }

        public Medication GetMedication()
        {
            _medications.UserId = BaseMapping.user_id;
            _medications.NameMedication = BaseMapping.medicine_name;
            _medications.PackageQuantity = BaseMapping.package_quantity;
            _medications.Dosage = BaseMapping.dosage;
            _medications.ExpirationDate = BaseMapping.expiration_date;
            _medications.IsActive = 1;
            return _medications;
        }
    }
}
