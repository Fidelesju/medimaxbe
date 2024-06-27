using System.Data.Common;
using Funq;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
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
                    p.id_proprietario AS OwnerId,
                    p.primeiro_nome AS FirstName,
                    p.ultimo_nome AS LastName,
                    p.email AS Email,
                    p.numero_telefone AS PhoneNumber,
                    p.endereco AS Address,
                    p.cidade AS City,
                    p.estado AS State,
                    p.codigo_postal AS PostalCode,
                    p.pais AS Country,
                    p.cpf_cnpj AS CpfCnpj,
                    p.esta_ativo AS IsActive
                FROM proprietarios p
                WHERE p.id_proprietario = {ownerId}
                AND p.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            owner = await GetQueryResultObject();
            await Disconnect();
            return owner;
        }
        public async Task<bool> DesactiveOwner ( int ownerId )
        {
            string sql;
            sql = $@"
                   UPDATE  proprietarios p 
                   SET p.esta_ativo = 0
                   WHERE p.id_proprietario = {ownerId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return true;
        }
        
        public async Task<bool> ReactiveOwner ( int ownerId )
        {
            string sql;
            sql = $@"
                   UPDATE  proprietarios p 
                   SET p.esta_ativo = 1
                   WHERE p.id_proprietario = {ownerId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return true;
        }
        public async Task<OwnerResponseModel> UpdateOwner ( OwnerUpdateRequestModel request )
        {
            string sql;
            OwnerResponseModel owner;
            sql = $@"
                   UPDATE proprietarios p 
                    SET 
                        p.primeiro_nome = '{request.FirstName}',
                        p.ultimo_nome = '{request.LastName}',
                        p.email = '{request.Email}',
                        p.numero_telefone = '{request.PhoneNumber}',
                        p.endereco = '{request.Address}',
                        p.cidade = '{request.City}',
                        p.estado = '{request.State}',
                        p.codigo_postal = '{request.PostalCode}',
                        p.pais = '{request.Country}',
                        p.cpf_cnpj = '{request.CpfCnpj}'
                    WHERE p.id_proprietario = {request.OwnerId}
                    AND p.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            owner = await GetQueryResultObject();
            await Disconnect();
            return owner;
        }

        protected override OwnerResponseModel Mapper(DbDataReader reader)
        {
            OwnerResponseModel _owner;
            _owner = new OwnerResponseModel();
            _owner.Id = Convert.ToInt32(reader["OwnerId"]);
            _owner.FirstName = Convert.ToString(reader["FirstName"]);
            _owner.LastName = Convert.ToString(reader["LastName"]);
            _owner.Email = Convert.ToString(reader["Email"]);
            _owner.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
            _owner.Address = Convert.ToString(reader["Address"]);
            _owner.City = Convert.ToString(reader["City"]);
            _owner.State = Convert.ToString(reader["State"]);
            _owner.PostalCode = Convert.ToString(reader["PostalCode"]);
            _owner.Country = Convert.ToString(reader["Country"]);
            _owner.CpfCnpj = Convert.ToString(reader["CpfCnpj"]);
            _owner.IsActive = Convert.ToInt32(reader["IsActive"]);
            return _owner;
        }
    }
}
