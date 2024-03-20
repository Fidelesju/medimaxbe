using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MediMax.Data.Repositories
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Owner owner)
        {
            DbSet.Add(owner);
            Context.SaveChanges();
            return owner.ownerId;
        }

        public void Update(Owner owner)
        {
            DbSet.Update(owner);
            Context.SaveChanges();
        }

        //public async Task<bool> Update(OwnerUpdateRequestModel request)
        //{
        //    //TODO Fazer Update
        //    string sql;
        //    int rowsAffected;

        //    try
        //    { 
        //        sql = $@"
        //        UPDATE owner
        //        SET 
        //            firstName = '{request.FirstName}',
        //            lastName = '{request.LastName}',
        //            email = '{request.Email}',
        //            phoneNumber = '{request.PhoneNumber}',
        //            address = '{request.Address}',
        //            city = '{request.City}',
        //            state = '{request.State}',
        //            postalCode = '{request.PostalCode}',
        //            country = '{request.Country}',
        //            CpfCnpj = '{request.CpfCnpj}'
        //        WHERE ownerId = {request.OwnerId}
        //        ";

        //        rowsAffected = await Context.Database.(sql, request.CpfCnpj);
        //        return rowsAffected > 0;
        //    }
        //    catch (MySqlException exception)
        //    {
        //        throw new DbUpdateException(exception.Message);
        //    }
        //}
    }
}
