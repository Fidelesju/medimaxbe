using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class MedicamentoDb : Db<MedicamentoResponseModel>, IMedicamentoDb
    {
        public MedicamentoDb(IConfiguration configuration,
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
                    m.dosagem AS Dosage,
                  m.esta_ativo AS IsActive
                 FROM medicamentos m
                 WHERE m.esta_ativo = 1
                ";

            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultList();
            await Disconnect();
            return medicamentoLista;
        }
        
        public async Task<List<MedicamentoResponseModel>> BuscarMedicamentosInativos ( )
        {
            string sql;
            List<MedicamentoResponseModel> medicamentoLista;
            sql = @"
                 SELECT 
	                m.id AS Id,
                    m.nome AS Name,
                    m.data_vencimento AS ExpirationDate,
                    m.quantidade_embalagem AS PackageQuantity,
                    m.dosagem AS Dosage,
                  m.esta_ativo AS IsActive
                 FROM medicamentos m
                 WHERE m.esta_ativo = 0
                ";

            await Connect();
            await Query(sql);
            medicamentoLista = await GetQueryResultList();
            await Disconnect();
            return medicamentoLista;
        }

        public async Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorNome(string name)
        {
            string sql;
            List<MedicamentoResponseModel> medicamento;
            sql = $@"
               SELECT 
                  m.id AS Id,
                  m.nome AS Name,
                  m.data_vencimento AS ExpirationDate,
                  m.quantidade_embalagem AS PackageQuantity,
                  m.dosagem AS Dosage,
                  m.esta_ativo AS IsActive
               FROM medicamentos m
               WHERE 
	              m.nome LIKE '%{name}%'
               AND m.esta_ativo = 1
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultList();
            await Disconnect();
            return medicamento;
        }
         public async Task<MedicamentoResponseModel> BuscarMedicamentosPorTratamento(int tratamentoId)
        {
            string sql;
            MedicamentoResponseModel medicamento;
            sql = $@"
                SELECT 
                    m.id AS Id,
                    m.nome AS Name,
                    m.data_vencimento AS ExpirationDate,
                    m.quantidade_embalagem AS PackageQuantity,
                    m.dosagem AS Dosage,
                    m.esta_ativo AS IsActive
                 FROM medicamentos m
                 INNER JOIN tratamento t ON t.remedio_id = m.id
                 WHERE 
                t.id = {tratamentoId}
                AND m.esta_ativo = 1
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultObject();
            await Disconnect();
            return medicamento;
        }
         
        public async Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorDataVencimento()
        {
            string sql;
            List<MedicamentoResponseModel> medicamento;
            sql = @"
               SELECT 
                  m.id AS Id,
                  m.nome AS Name,
                  m.data_vencimento AS ExpirationDate,
                  m.quantidade_embalagem AS PackageQuantity,
                  m.dosagem AS Dosagem,
                  m.esta_ativo AS IsActive
               FROM medicamentos m
               WHERE m.esta_ativo = 1
               ORDER BY 
	              m.data_vencimento ASC
                ";

            await Connect();
            await Query(sql);
            medicamento = await GetQueryResultList();
            await Disconnect();
            return medicamento;
        }

        public async Task<bool> DeletandoMedicamento(int id)
        {
            string sql;
            bool success;
            sql = $@"
               UPDATE medicamentos m
                SET m.esta_ativo = 0
                WHERE m.id = {id}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }
        
        public async Task<bool> AlterandoMedicamento(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id)
        {
            string sql;
            bool success;
            sql = $@"
               UPDATE medicamentos m
               SET m.nome = '{nome}', m.data_vencimento = '{data_vencimento}', m.quantidade_embalagem = {quantidade_embalagem}, m.dosagem = {dosagem}
               WHERE m.id = {id}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }

        protected override MedicamentoResponseModel Mapper(DbDataReader reader)
        {
            MedicamentoResponseModel medicine = new MedicamentoResponseModel();

            // Convertendo Id para int
            if (int.TryParse(reader["Id"].ToString(), out int id))
            {
                medicine.Id = id;
            }
            else
            {
                // Tratar erro de conversão
            }

            // Convertendo Name para string
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
