using System.Data.Common;
using AutoMapper;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using ServiceStack;

namespace MediMax.Data.Dao
{
    public class MedicationDb : Db<MedicationResponseModel>, IMedicationDb
    {
        private IMapper _mapper;
        public MedicationDb(
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<List<MedicationResponseModel>> GetAllMedicine(int userId)
        {
            string sql;
            List<MedicationResponseModel> medicamentoLista;
            sql = $@"
                SELECT 
                   m.id AS Id,
                   m.name_medication AS NameMedication,
                   m.expiration_date AS ExpirationDate,
                   m.package_quantity AS PackageQuantity,
                   m.dosage AS Dosage,
                   m.is_active AS IsActive,
                   m.user_id AS UserId
                FROM medication m
                WHERE m.is_active = 1
                AND m.user_id = {userId}
                ORDER BY m.id DESC;

                ";

            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultList();
            await Disconnect();
            return medicamentoLista;
        }
        
        public async Task<List<MedicationResponseModel>> BuscarMedicamentosInativos ( int userId )
        {
            string sql;
            List<MedicationResponseModel> medicamentoLista;
            sql = @"
                 SELECT 
                   m.id AS Id,
                   m.name_medication AS NameMedication,
                   m.expiration_date AS ExpirationDate,
                   m.package_quantity AS PackageQuantity,
                   m.dosage AS Dosage,
                   m.is_active AS IsActive,
                   m.user_id AS UserId
                FROM medication m
                WHERE m.is_active = 0
                AND m.user_id = {userId}
                ORDER BY m.id DESC;
                ";

            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultList();
            await Disconnect();
            return medicamentoLista;
        }

        public async Task<List<MedicationResponseModel>> GetMedicationByName(string name, int userId)
        {
            string sql;
            List<MedicationResponseModel> medicamento;
            sql = $@"
                SELECT 
                   m.id AS Id,
                   m.name_medication AS NameMedication,
                   m.expiration_date AS ExpirationDate,
                   m.package_quantity AS PackageQuantity,
                   m.dosage AS Dosage,
                   m.is_active AS IsActive,
                   m.user_id AS UserId
                FROM medication m
                WHERE m.is_active = 1
                AND m.user_id = {userId}
	            AND m.name_medication LIKE '%{name}%'
                ORDER BY m.id DESC;
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultList();
            await Disconnect();
            return medicamento;
        }
         public async Task<MedicationResponseModel> GetMedicationByTreatmentId(int TreatmentId, int userId)
        {
            string sql;
            MedicationResponseModel medicamento;
            sql = $@"
                SELECT 
                   m.id AS Id,
                   m.name_medication AS NameMedication,
                   m.expiration_date AS ExpirationDate,
                   m.package_quantity AS PackageQuantity,
                   m.dosage AS Dosage,
                   m.is_active AS IsActive,
                   m.user_id AS UserId
                FROM medication m
                INNER JOIN treatment t ON t.medication_id = m.id
                WHERE t.id = {TreatmentId}
                AND m.is_active = 1
                AND m.user_id = {userId}
                ORDER BY m.id DESC;
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultObject();
            await Disconnect();
            return medicamento;
        }
         
        public async Task<List<MedicationResponseModel>> GetMedicationByExpirationDate(int userId)
        {
            string sql;
            List<MedicationResponseModel> medicamento;
            sql = $@"
               SELECT 
                   m.id AS Id,
                   m.name_medication AS NameMedication,
                   m.expiration_date AS ExpirationDate,
                   m.package_quantity AS PackageQuantity,
                   m.dosage AS Dosage,
                   m.is_active AS IsActive,
                   m.user_id AS UserId
                FROM medication m
               WHERE m.is_active = 1
               AND m.user_id = {userId}
               ORDER BY 
	              m.expiration_date ASC
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultList();
            await Disconnect();
            return medicamento;
        }

        protected override MedicationResponseModel Mapper(DbDataReader reader)
        {
           return _mapper.Map<MedicationResponseModel>(reader);
        }

    }
}
