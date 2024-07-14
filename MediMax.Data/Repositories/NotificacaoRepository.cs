using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories
{
    public class NotificationRepository: Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Notification Notification)
        {
            DbSet.Add(Notification);
            Context.SaveChanges();
            return Notification.id;
        }

        public void Update(Notification Notification)
        {
            DbSet.Update(Notification);
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
