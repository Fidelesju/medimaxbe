using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class MedicamentoCreateMapper : Mapper<MedicamentoETreatmentCreateRequestModel>, IMedicamentoCreateMapper
    {
        private readonly Medicamentos? _medications;

        public MedicamentoCreateMapper()
        {
            _medications = new Medicamentos();
        }

        public Medicamentos GetMedicamentos()
        {
            _medications.nome = BaseMapping.nome;
            _medications.quantidade_embalagem = BaseMapping.quantidade_embalagem;
            _medications.dosagem = BaseMapping.dosagem;
            _medications.data_vencimento = BaseMapping.data_vencimento_medication;
            return _medications;
        }
    }
}
