using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class MedicamentoCreateMapper : Mapper<MedicamentoETratamentoCreateRequestModel>, IMedicamentoCreateMapper
    {
        private readonly Medicamentos? _medicamentos;

        public MedicamentoCreateMapper()
        {
            _medicamentos = new Medicamentos();
        }

        public Medicamentos BuscarMedicamentos()
        {
            _medicamentos.usuarioId = BaseMapping.usuarioId;
            _medicamentos.nome = BaseMapping.nome;
            _medicamentos.quantidade_embalagem = BaseMapping.quantidade_embalagem;
            _medicamentos.dosagem = BaseMapping.dosagem;
            _medicamentos.data_vencimento = BaseMapping.data_vencimento_medicamento;
            _medicamentos.esta_ativo = 1;
            return _medicamentos;
        }
    }
}
