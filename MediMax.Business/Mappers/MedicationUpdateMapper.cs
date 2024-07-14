using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class MedicationUpdateMapper : Mapper<MedicationUpdateRequestModel>, IMedicationUpdateMapper
    {
        private readonly Medicamentos? _medications;

        public MedicationUpdateMapper ( )
        {
            _medications = new Medicamentos();
        }

        public Medicamentos GetMedication()
        {
            _medications.id = BaseMapping.medication_id;
            _medications.usuarioId = BaseMapping.user_id;
            _medications.nome = BaseMapping.medicine_name;
            _medications.quantidade_embalagem = BaseMapping.package_quantity;
            _medications.dosagem = BaseMapping.dosage;
            _medications.data_vencimento = BaseMapping.expiration_date;
            _medications.esta_ativo = 1;
            return _medications;
        }
    }
}
