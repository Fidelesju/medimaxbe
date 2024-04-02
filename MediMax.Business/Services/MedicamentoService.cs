using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace MediMax.Business.Services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoCreateMapper _medicamentoCreateMapper;
        private readonly ITratamentoCreateMapper _tratamentoCreateMapper;
        private readonly IMedicamentosRepository _medicineRepository;
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly IMedicineDb _medicamentoDb;

        public MedicamentoService(
            IMedicamentoCreateMapper medicineCreateMapper,
            ITratamentoCreateMapper tratamentoCreateMapper,
            IMedicamentosRepository medicineRepository,
            ITratamentoRepository tratamentoRepository,
            IMedicineDb medicineDb)
        {
            _medicamentoCreateMapper = medicineCreateMapper;
            _medicineRepository = medicineRepository;
            _medicamentoDb = medicineDb;
            _tratamentoCreateMapper = tratamentoCreateMapper;
            _tratamentoRepository = tratamentoRepository;
        }

        /// <summary>
        /// Cria um novo medicamento e tratamento.
        /// </summary>
        public async Task<int> CriandoMedicamentosETratamento(MedicamentoETratamentoCreateRequestModel request)
        {
            Tratamento tratamentos;
            Medicamentos medicamentos;
            MedicamentoCreateValidation validation = new MedicamentoCreateValidation();
            Dictionary<string, string> errors;

            _medicamentoCreateMapper.SetBaseMapping(request);
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                medicamentos = _medicamentoCreateMapper.BuscarMedicamentos();
                tratamentos = _tratamentoCreateMapper.BuscarTratemento(request);

                _medicineRepository.Create(medicamentos);
                _tratamentoRepository.Create(tratamentos);

                return medicamentos.id;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                if (errors.Count == 0)
                {
                    throw;
                }
                throw new CustomValidationException(errors);
            }
        }

        /// <summary>
        /// Obtém todos os medicamentos.
        /// </summary>
        public async Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos()
        {
            List<MedicamentoResponseModel> medicamentoLista;
            medicamentoLista = await _medicamentoDb.BuscarTodosMedicamentos();

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                throw new RecordNotFoundException();
            }
            return medicamentoLista;
        }
    }
}
