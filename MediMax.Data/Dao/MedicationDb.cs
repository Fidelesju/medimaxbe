using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using ServiceStack;

namespace MediMax.Data.Dao
{
    public class MedicationDb : Db<MedicationResponseModel>, IMedicationDb
    {
        public MedicationDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<MedicationResponseModel>> GetAllMedicine(int userId)
        {
            string sql;
            List<MedicationResponseModel> medicamentoLista;
            sql = $@"
                 	SELECT 
	                   m.id AS Id,
	                   m.nome AS Name,
	                   m.data_vencimento AS ExpirationDate,
	                   m.quantidade_embalagem AS PackageQuantity,
	                   m.dosagem AS Dosage,
	                   m.esta_ativo AS IsActive,
	                   m.usuarioId AS UserId
	                FROM medicamentos m
	                WHERE m.esta_ativo = 1
	                AND m.usuarioId = {userId}
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
                    m.nome AS Name,
                    m.data_vencimento AS ExpirationDate,
                    m.quantidade_embalagem AS PackageQuantity,
                    m.dosagem AS Dosage,
                  m.esta_ativo AS IsActive,
                  m.usuarioId AS UserId
                 FROM medicamentos m
                 WHERE m.esta_ativo = 0
                 AND m.usuarioId = {userId}
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
                  m.nome AS Name,
                  m.data_vencimento AS ExpirationDate,
                  m.quantidade_embalagem AS PackageQuantity,
                  m.dosagem AS Dosage,
                  m.esta_ativo AS IsActive,
                  m.usuarioId AS UserId
               FROM medicamentos m
               WHERE 
	              m.nome LIKE '%{name}%'
                AND m.esta_ativo = 1
                AND m.usuarioId = {userId}
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
                    m.nome AS Name,
                    m.data_vencimento AS ExpirationDate,
                    m.quantidade_embalagem AS PackageQuantity,
                    m.dosagem AS Dosage,
                    m.esta_ativo AS IsActive,
                    m.usuarioId AS UserId
                 FROM medicamentos m
                 INNER JOIN tratamento t ON t.remedio_id = m.id
                WHERE t.id = {TreatmentId}
                AND m.esta_ativo = 1
                AND m.usuarioId = {userId}
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
                  m.nome AS Name,
                  m.data_vencimento AS ExpirationDate,
                  m.quantidade_embalagem AS PackageQuantity,
                  m.dosagem AS Dosage,
                  m.esta_ativo AS IsActive,
                  m.usuarioId AS UserId
               FROM medicamentos m
               WHERE m.esta_ativo = 1
               AND m.usuarioId = {userId}
               ORDER BY 
	              m.data_vencimento ASC
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultList();
            await Disconnect();
            return medicamento;
        }

        public async Task<bool> DeletandoMedicamento(int id, int userId)
        {
            string sql;
            bool success;
            sql = $@"
               UPDATE medicamentos m
                SET m.esta_ativo = 0
                WHERE m.id = {id}
                AND m.UserId = {userId}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }
        
        public async Task<bool> AlterandoMedicamento(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id, int userId)
        {
            string sql;
            bool success;
            sql = $@"
               UPDATE medicamentos m
               SET m.nome = '{nome}', m.data_vencimento = '{data_vencimento}', m.quantidade_embalagem = {quantidade_embalagem}, m.dosagem = {dosagem}
               WHERE m.id = {id}
               AND m.UserId = {userId}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        protected override MedicationResponseModel Mapper(DbDataReader reader)
        {
            MedicationResponseModel medicine = new MedicationResponseModel();

            // Convertendo Id para int
            if (int.TryParse(reader["Id"].ToString(), out int id))
            {
                medicine.Id = id;
            }
            else
            {
                // Tratar erro de conversão
            } 
            
            if (int.TryParse(reader["UserId"].ToString(), out int userId))
            {
                medicine.UserId = id;
            }
            else
            {
                // Tratar erro de conversão
            }

            medicine.Name = reader["Name"].ToString();


            // Convertendo ExpirationDate para string
            medicine.ExpirationDate = reader["ExpirationDate"].ToString();

            // Convertendo Dosage para double
            if (double.TryParse(reader["Dosage"].ToString(), out double dosage))
            {
                medicine.Dosage = dosage;
            }
            else
            {
                // Tratar erro de conversão
            }
          
            // Convertendo PackageQuantity para int
            if (int.TryParse(reader["PackageQuantity"].ToString(), out int packageQuantity))
            {
                medicine.PackageQuantity = packageQuantity;
            }
            else
            {
                // Tratar erro de conversão
            } 
            // Convertendo PackageQuantity para int
            if (int.TryParse(reader["IsActive"].ToString(), out int isActive))
            {
                medicine.IsActive = isActive;
            }
            else
            {
                // Tratar erro de conversão
            }
            return medicine;
        }

    }
}
