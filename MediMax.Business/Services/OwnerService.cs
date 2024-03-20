using Microsoft.EntityFrameworkCore;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using MediMax.Data.Dao;
using MediMax.Data.ApplicationModels;

namespace MediMax.Business.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerCreateMapper _ownerCreateMapper;
        private readonly IOwnerUpdateMapper _ownerUpdateMapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IOwnerDb _ownerDb;
        public OwnerService(
            IOwnerCreateMapper ownerCreateMapper,
            IOwnerUpdateMapper ownerUpdateMapper,
            IOwnerRepository ownerRepository,
            IOwnerDb ownerDb) 
        {
            _ownerCreateMapper = ownerCreateMapper;
            _ownerRepository = ownerRepository;
            _ownerDb = ownerDb;
            _ownerUpdateMapper = ownerUpdateMapper;
        }

        public async Task<int> CreateOwner(OwnerCreateRequestModel request)
        {
            Owner owner;
            OwnerCreateValidation validation;
            Dictionary<string, string> errors;

            _ownerCreateMapper.SetBaseMapping(request);
            validation = new OwnerCreateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }
            try
            {
                owner = _ownerCreateMapper.GetOwner();
                _ownerRepository.Create(owner);
                return owner.ownerId;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                if (errors.Count == 0)
                {
                    throw;
                }
                throw new CustomValidationException(errors);
            }
        }

        public async Task<OwnerResponseModel> GetOwnerById(int ownerId)
        {
            OwnerResponseModel owner;
            owner = await _ownerDb.GetOwnerById(ownerId);
            if (owner == null)
            {
                throw new RecordNotFoundException();
            }
            return owner;
        }


        public async Task<PaginatedList<OwnerResponseModel>> GetOwnerPaginatedList(
           Pagination pagination)
        {
            PaginatedList<OwnerResponseModel> ownerList;
            ownerList = await _ownerDb.GetPaginatedListOwners(pagination);
            if (PaginatedList<OwnerResponseModel>.IsEmpty(ownerList))
            {
                throw new RecordNotFoundException();
            }

            return ownerList;
        }

        public async Task<PaginatedList<OwnerResponseModel>> GetPaginatedListDesactivesOwner(
          Pagination pagination)
        {
            PaginatedList<OwnerResponseModel> ownerList;
            ownerList = await _ownerDb.GetPaginatedListDesactivesOwner(pagination);
            if (PaginatedList<OwnerResponseModel>.IsEmpty(ownerList))
            {
                throw new RecordNotFoundException();
            }

            return ownerList;
        }

        //public async Task<bool> UpdateOwner(OwnerUpdateRequestModel request)
        //{
        //    Owner owner;
        //    OwnerUpdateValidation validation;
        //    Dictionary<string, string> errors;

        //    _ownerUpdateMapper.SetBaseMapping(request);
        //    validation = new OwnerUpdateValidation();
        //    if (!validation.IsValid(request))
        //    {
        //        errors = validation.GetErrors();
        //        throw new CustomValidationException(errors);
        //    }
        //    try
        //    {
        //        owner = _ownerUpdateMapper.GetOwner();
        //        _ownerRepository.Update(request);
        //        return true;
        //    }
        //    catch (DbUpdateException exception)
        //    {
        //        errors = validation.GetPersistenceErrors(exception);
        //        if (errors.Count == 0)
        //        {
        //            throw;
        //        }
        //        throw new CustomValidationException(errors);
        //    }
        //}
    }
}
