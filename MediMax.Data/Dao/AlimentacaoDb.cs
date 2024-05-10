using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class AlimentacaoDb : Db<AlimentacaoResponseModel>, IAlimentacaoDb
    {
        public AlimentacaoDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals)
        {
            string sql;
            List<AlimentacaoResponseModel> alimentacaoList;
            sql = $@"
              SELECT 
                  a.id AS Id,
                  a.tipo_refeicao AS TypeMeals,
                  a.horario AS Time,
                  a.alimento AS Meals,
                  a.quantidade AS QuantityMeals,
                  a.unidade_medida AS Unit
               FROM alimentacao a
               WHERE a.tipo_refeicao = '{typeMeals}'
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
        
        public async Task<List<AlimentacaoResponseModel>> BuscarTodasAlimentacao()
        {
            string sql;
            List<AlimentacaoResponseModel> alimentacaoList;
            sql = $@"
              SELECT 
                 a.id AS Id,
                 a.tipo_refeicao AS TypeMeals,
                 a.horario AS Time,
                 a.alimento AS Meals,
                 a.quantidade AS QuantityMeals,
                 a.unidade_medida AS Unit
              FROM alimentacao a
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
         public async Task<AlimentacaoResponseModel> BuscarRefeicoesPorHorario ( )
        {
            string sql;
            AlimentacaoResponseModel alimentacaoList;
            sql = $@"
              SELECT 
                    a.id AS Id,
                    a.tipo_refeicao AS TypeMeals,
                    a.horario AS Time,
                    a.alimento AS Meals,
                    a.quantidade AS QuantityMeals,
                    a.unidade_medida AS Unit
                FROM alimentacao a
                WHERE 
                    a.horario >= NOW()  -- Horário atual ou futuro
                ORDER BY a.horario ASC -- Ordena pelo horário mais próximo primeiro
                LIMIT 1
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
                UPDATE 
                    alimentacao a
                SET a.tipo_refeicao = '{request.tipo_refeicao}', a.horario = '{request.horario}', a.quantidade = {request.quantidade}, a.alimento = '{request.alimento}', a.unidade_medida = '{request.unidade_medida}'
                WHERE a.id = {request.id}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        } 
      

        public async Task<bool> DeletandoAlimentacao(int id)
        {
            string sql;
            bool success;
            sql = $@"
                DELETE FROM alimentacao a WHERE a.id = {id}
                ";
            await Connect();
            success = await PersistQuery(sql);
            await Disconnect();
            return success;
        }
        protected override AlimentacaoResponseModel Mapper(DbDataReader reader)
        {
            AlimentacaoResponseModel alimentacao;
            alimentacao = new AlimentacaoResponseModel();
            alimentacao.id = Convert.ToInt32(reader["Id"]);
            alimentacao.alimento = Convert.ToString(reader["Meals"]);
            alimentacao.horario = Convert.ToString(reader["Time"]);
            alimentacao.tipo_refeicao = Convert.ToString(reader["TypeMeals"]);
            alimentacao.unidade_medida = Convert.ToString(reader["Unit"]);
            alimentacao.quantidade = Convert.ToInt32(reader["QuantityMeals"]);
            return alimentacao;
        }
    }
}
