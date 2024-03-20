using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Application.Controller
{
    [Route("[controller]"), ApiController]
    public class OwnerController : BaseController<OwnerController>
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(
            ILogger<OwnerController> logger,
            ILoggerService loggerService,
            IOwnerService ownerService) : base(logger, loggerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Creating owners
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponse<int>>> CreateOwner(OwnerCreateRequestModel request)
        {
            BaseResponse<int> response;
            int id;
            try
            {
                id = await _ownerService.CreateOwner(request);
                if (id == 0)
                {
                    response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Falha ao cadastrar um proprietário.")
                        .SetData(0)
                    ;
                    return BadRequest(response);
                }

                response = BaseResponse<int>
                        .Builder()
                        .SetMessage("Proprietário criado com sucesso.")
                        .SetData(id)
                    ;
                return Ok(response);
            }
            catch (CustomValidationException exception)
            {
                return ValidationErrorsBadRequest(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }


        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("{ownerId}")]
        public async Task<ActionResult<BaseResponse<OwnerResponseModel>>> GetOwnerById(int ownerId)
        {
            OwnerResponseModel owner;
            BaseResponse<OwnerResponseModel> response;
            try
            {
                owner = await _ownerService.GetOwnerById(ownerId);
                response = BaseResponse<OwnerResponseModel>
                        .Builder()
                        .SetMessage("Owner encontrado com sucesso.")
                        .SetData(owner)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("Page/{page}/PerPage/{perPage}")]
        public async Task<ActionResult<OwnerResponseModel>> GetPaginatedListOwner(int page, int perPage)
        {
            PaginatedList<OwnerResponseModel> ownerList;
            BaseResponse<PaginatedList<OwnerResponseModel>> response;
            Pagination pagination;
            try
            {
                pagination = Pagination
                        .Builder()
                        .SetCurrentPage(page)
                        .SetPerPage(perPage)
                    ;
                ownerList = await _ownerService.GetOwnerPaginatedList(pagination);
                response = BaseResponse<PaginatedList<OwnerResponseModel>>
                        .Builder()
                        .SetMessage("Owners encontrados com sucesso.")
                        .SetData(ownerList)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        [/*Authorize(Roles = "owner,admin"),*/ HttpGet("Desactives/Page/{page}/PerPage/{perPage}")]
        public async Task<ActionResult<OwnerResponseModel>> GetPaginatedListDesactivesOwner(int page, int perPage)
        {
            PaginatedList<OwnerResponseModel> ownerList;
            BaseResponse<PaginatedList<OwnerResponseModel>> response;
            Pagination pagination;
            try
            {
                pagination = Pagination
                        .Builder()
                        .SetCurrentPage(page)
                        .SetPerPage(perPage)
                    ;
                ownerList = await _ownerService.GetPaginatedListDesactivesOwner(pagination);
                response = BaseResponse<PaginatedList<OwnerResponseModel>>
                        .Builder()
                        .SetMessage("Owners encontrados com sucesso.")
                        .SetData(ownerList)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }

        //[/*Authorize(Roles = "owner,admin"),*/ HttpPut("Update")]
        //public async Task<ActionResult<BaseResponse<string>>> UpdateOwner(OwnerUpdateRequestModel request)
        //{
        //    BaseResponse<string> response;
        //    bool success;
        //    try
        //    {
                
        //        success = await _ownerService.UpdateOwner(request);
        //        if (!success)
        //        {
        //            response = BaseResponse<string>
        //                    .Builder()
        //                    .SetMessage("Erro na atualização de proprietario.")
        //                    .SetData("")
        //                ;
        //            return BadRequest(response);
        //        }

        //        response = BaseResponse<string>
        //                .Builder()
        //                .SetMessage("Proprietario atualizado com sucesso")
        //                .SetData("")
        //            ;
        //        return Ok(response);
        //    }
        //    catch (CustomValidationException exception)
        //    {
        //        return ValidationErrorsBadRequest(exception);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await UntreatedException(exception);
        //    }
        //}

        [/*Authorize(Roles = "owner,admin"),*/ HttpPost("Desactive/{ownerId}")]
        public async Task<ActionResult<BaseResponse<OwnerResponseModel>>> DesactiveOwner(int ownerId)
        {
            OwnerResponseModel owner;
            BaseResponse<OwnerResponseModel> response;
            try
            {
                owner = await _ownerService.GetOwnerById(ownerId);
                response = BaseResponse<OwnerResponseModel>
                        .Builder()
                        .SetMessage("Owner encontrado com sucesso.")
                        .SetData(owner)
                    ;
                return Ok(response);
            }
            catch (RecordNotFoundException exception)
            {
                return await NotFoundResponse(exception);
            }
            catch (Exception exception)
            {
                return await UntreatedException(exception);
            }
        }
    }
}
