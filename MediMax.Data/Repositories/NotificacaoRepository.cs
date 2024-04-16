using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories
{
    public class NotificacaoRepository: Repository<Notificacao>, INotificacaoRepository
    {
        public NotificacaoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Notificacao notificacao)
        {
            DbSet.Add(notificacao);
            Context.SaveChanges();
            return notificacao.id;
        }

        public void Update(Notificacao notificacao)
        {
            DbSet.Update(notificacao);
            Context.SaveChanges();
        }

        //public async Task<int> SetToRead(int notificationId)
        //{
        //    int rowsAffected;
        //    string sql = $@"
        //        UPDATE notification
        //        SET notification.is_open = 1
        //        WHERE
        //        notification.id = {notificationId}";

        //    await Connect();
        //    await Query(sql);
        //    rowsAffected = await GetQueryResultObject();
        //    await Disconnect();
        //    return rowsAffected;
        //}
        //public async Task<int> SetToReadAll(int ownerId)
        //{
        //    int rowsAffected;
        //    string sql = @"
        //        UPDATE notification
        //        SET notification.is_open = 1
        //        WHERE
        //        notification.owner_id = {0}";

        //    rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, ownerId);
        //    return rowsAffected;
        //}
    }
}
