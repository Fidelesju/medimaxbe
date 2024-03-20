using System.Data.Common;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class OwnerDb : Db<OwnerResponseModel>, IOwnerDb
    {
        public OwnerDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<OwnerResponseModel> GetOwnerById(int ownerId)
        {
            string sql;
            OwnerResponseModel owner;
            sql = $@"
                 SELECT 
                    o.ownerId AS ownerId,
                    o.firstName AS firstName,
                    o.lastName AS lastName,
                    o.email AS email,
                    o.phoneNumber AS phoneNumber,
                    o.address AS address,
                    o.city AS city,
                    o.state AS state,
                    o.postalCode AS postalCode,
                    o.country AS country,
                    o.CpfCnpj AS CpfCnpj,
                    o.isActive AS isActive
                FROM owner o
                WHERE o.ownerId = {ownerId}
                    AND o.isActive = 1
                ;";

            await Connect();
            await Query(sql);
            owner = await GetQueryResultObject();
            await Disconnect();
            return owner;
        }

        public async Task<PaginatedList<OwnerResponseModel>> GetPaginatedListOwners(Pagination pagination)
        {
            string sql;
            PaginatedList<OwnerResponseModel> owner;
            sql = @"
               SELECT 
                    o.ownerId AS ownerId,
                    o.firstName AS firstName,
                    o.lastName AS lastName,
                    o.email AS email,
                    o.phoneNumber AS phoneNumber,
                    o.address AS address,
                    o.city AS city,
                    o.state AS state,
                    o.postalCode AS postalCode,
                    o.country AS country,
                    o.CpfCnpj AS CpfCnpj,
                    o.isActive AS isActive
                FROM owner o
                WHERE o.isActive = 1
            ";
            await Connect();
            SetPagination(pagination);
            await Query(sql);
            owner = new PaginatedList<OwnerResponseModel>();
            owner.List = await GetQueryResultList();
            owner.Pagination = await GetPagination();
            await Disconnect();
            return owner;
        }

        public async Task<PaginatedList<OwnerResponseModel>> GetPaginatedListDesactivesOwner(Pagination pagination)
        {
            string sql;
            PaginatedList<OwnerResponseModel> owner;
            sql = @"
               SELECT 
                    o.ownerId AS ownerId,
                    o.firstName AS firstName,
                    o.lastName AS lastName,
                    o.email AS email,
                    o.phoneNumber AS phoneNumber,
                    o.address AS address,
                    o.city AS city,
                    o.state AS state,
                    o.postalCode AS postalCode,
                    o.country AS country,
                    o.CpfCnpj AS CpfCnpj,
                    o.isActive AS isActive
                FROM owner o
                WHERE o.isActive = 0
            ";
            await Connect();
            SetPagination(pagination);
            await Query(sql);
            owner = new PaginatedList<OwnerResponseModel>();
            owner.List = await GetQueryResultList();
            owner.Pagination = await GetPagination();
            await Disconnect();
            return owner;
        }

        protected override OwnerResponseModel Mapper(DbDataReader reader)
        {
            OwnerResponseModel _owner;
            _owner = new OwnerResponseModel();
            _owner.Id = Convert.ToInt32(reader["ownerId"]);
            _owner.FirstName = Convert.ToString(reader["firstName"]);
            _owner.LastName = Convert.ToString(reader["lastName"]);
            _owner.Email = Convert.ToString(reader["email"]);
            _owner.PhoneNumber = Convert.ToString(reader["phoneNumber"]);
            _owner.Address = Convert.ToString(reader["address"]);
            _owner.City = Convert.ToString(reader["city"]);
            _owner.State = Convert.ToString(reader["state"]);
            _owner.PostalCode = Convert.ToString(reader["postalCode"]);
            _owner.Country = Convert.ToString(reader["country"]);
            _owner.CpfCnpj = Convert.ToString(reader["CpfCnpj"]);
            _owner.IsActive = Convert.ToInt32(reader["isActive"]);
            return _owner;
        }
    }
}
