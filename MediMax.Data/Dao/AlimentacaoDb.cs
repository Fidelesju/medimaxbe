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
