using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class MedicineDb : Db<MedicamentoResponseModel>, IMedicineDb
    {
        public MedicineDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos()
        {
            string sql;
            List<MedicamentoResponseModel> medicamentoLista;
            sql = @"
               SELECT 
	                m.id AS Id,
                    m.nome AS Name,
                    m.data_vencimento AS ExpirationDate,
                    m.quantidade_embalagem AS PackageQuantity,
                    m.dosagem AS Dosage
                 FROM medicamentos m
                ";

            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultList();
            await Disconnect();
            return medicamentoLista;
        }


        protected override MedicamentoResponseModel Mapper(DbDataReader reader)
        {
            MedicamentoResponseModel medicine;
            medicine = new MedicamentoResponseModel();
            medicine.Id = Convert.ToInt32(reader["Id"]);
            medicine.Name = Convert.ToString(reader["Name"]);
            medicine.ExpirationDate = Convert.ToString(reader["ExpirationDate"]);
            medicine.Dosage = Convert.ToDouble(reader["Dosage"]);
            medicine.PackageQuantity = Convert.ToInt32(reader["PackageQuantity"]);
            return medicine;
        }
    }
}
