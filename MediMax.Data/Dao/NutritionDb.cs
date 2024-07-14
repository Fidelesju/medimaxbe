using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class NutritionDb : Db<NutritionResponseModel>, INutritionDb
    {
        public NutritionDb(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, 
            MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<NutritionResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals, int userId)
        {
            string sql;
            List<NutritionResponseModel> alimentacaoList;
            sql = $@"
                  SELECT 
                   a.id AS Id,
                   a.tipo_refeicao AS TypeMeals,
                   a.horario AS Time,
                   da.alimento AS Meals,
                   da.quantidade AS QuantityMeals,
                   da.unidade_medida AS Unit,
                   a.UserId AS UserId,
                   da.id AS DetaiMealsId
                FROM alimentacao a
                INNER JOIN detalhe_alimentacao da ON da.id = a.detalhe_alimentacao_id
                WHERE a.tipo_refeicao = '{typeMeals}'
                AND a.UserId = {userId};
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
        
        public async Task<List<NutritionResponseModel>> BuscarTodasAlimentacao(int userId)
        {
            string sql;
            List<NutritionResponseModel> alimentacaoList;
            sql = $@"
              SELECT 
                 a.id AS Id,
                 a.tipo_refeicao AS TypeMeals,
                 a.horario AS Time,
                 da.alimento AS Meals,
                da.quantidade AS QuantityMeals,
                da.unidade_medida AS Unit,
                da.id AS DetaiMealsId,
                 a.UserId AS UserId
              FROM alimentacao a
              INNER JOIN detalhe_alimentacao da ON da.id = a.detalhe_alimentacao_id
              AND a.UserId = {userId};
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
        public async Task<NutritionResponseModel> BuscarRefeicoesPorHorario ( int userId)
        {
            string sql;
            NutritionResponseModel alimentacaoList;
            sql = $@"
                     SELECT 
	                    a.id AS Id,
	                    a.tipo_refeicao AS TypeMeals,
	                    a.horario AS Time,
                       da.alimento AS Meals,
                       da.quantidade AS QuantityMeals,
                       da.unidade_medida AS Unit,
	                    a.UserId AS UserId
                    FROM alimentacao a
                    INNER JOIN detalhe_alimentacao da ON da.id = a.detalhe_alimentacao_id
                    WHERE 
	                    a.horario >= NOW()  -- Horário atual ou futuro
                    AND a.UserId = 1
                    ORDER BY a.horario ASC -- Ordena pelo horário mais próximo primeiro
                    LIMIT 1;
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultObject();
            await Disconnect();
            return alimentacaoList;
        }

        public async Task<bool> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request)
        {
            string sql;
            bool success;
            sql = $@"
             UPDATE alimentacao a
                SET 
                    a.tipo_refeicao = '{request.tipo_refeicao}',
                    a.horario = '{request.horario}'
                WHERE a.id = {request.id}
                AND a.UserId = {request.UserId}

                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
       }
        
        public async Task<bool> AlterandoDetalheAlimentacao(string quantidade, string alimento, string unidade_medida, int id)
        {
            string sql;
            bool success;
            sql = $@"
             UPDATE detalhe_alimentacao da
                SET 
                    da.quantidade = '{quantidade}',
                    da.alimento = '{alimento}',
                    da.unidade_medida = '{unidade_medida}'
                WHERE da.id = {id}

                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        } 
      
        public async Task<bool> DeletandoAlimentacao(int id, int userId)
        {
            string sql;
            bool success;
            sql = $@"
                DELETE FROM alimentacao a 
                WHERE a.id = {id}
                AND a.UserId = {userId}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }
        protected override NutritionResponseModel Mapper(DbDataReader reader)
        {
            NutritionResponseModel alimentacao;
            DetalheAlimentacao detalheAlimentacao;
            alimentacao = new NutritionResponseModel();
            detalheAlimentacao = new DetalheAlimentacao();
            alimentacao.id = Convert.ToInt32(reader["Id"]);
            detalheAlimentacao.alimento = Convert.ToString(reader["Meals"]);
            alimentacao.horario = Convert.ToString(reader["Time"]);
            alimentacao.tipo_refeicao = Convert.ToString(reader["TypeMeals"]);
            detalheAlimentacao.unidade_medida = Convert.ToString(reader["Unit"]);
            detalheAlimentacao.quantidade = Convert.ToString(reader["QuantityMeals"]);
            alimentacao.UserId = Convert.ToInt32(reader["UserId"]);
            detalheAlimentacao.id = Convert.ToInt32(reader["DetaiMealsId"]);

            alimentacao.detalhe_alimentacao_id = detalheAlimentacao;
            return alimentacao;
        }
    }
}
